using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Boid : MonoBehaviour
{
    private Vector3 avoidance, alignment;
    private Vector3 velocity;
    [SerializeField] private float speed = 1f,
        avoidanceRadius = 1f, magnetRadius = 10f;

    private List<Boid> boids = new List<Boid>();
    void Start()
    {
        boids = BoidMaster.boids;
    }

    // Update is called once per frame
    void Update()
    {
        velocity.Normalize();
        velocity += (Avoidance()*10 + Alignement()*5 + Cohesion()).normalized;
        if (velocity.sqrMagnitude < 0.3f) velocity = transform.up ;
        RotateTowardsDirection();        
        transform.position += velocity*(Time.deltaTime*speed);
    }

    private void RotateTowardsDirection()
    {
        if (velocity.sqrMagnitude < 0.25f) return;
        float angle = Mathf.Clamp(Vector3.Angle(transform.up, velocity), -90, 90);
        transform.rotation *= Quaternion.Euler(0, 0, angle);
    }
    private Vector3 Avoidance()
    {
        Vector3 c = Vector3.zero;
        List<Boid> closest =
            boids.FindAll(b => 
                b != this
                && (b.transform.position- transform.position).sqrMagnitude < Mathf.Pow(avoidanceRadius,2));
        foreach (Boid friend in closest)
        {
            if(friend == this) continue;
            c -= (friend.transform.position - transform.position);
        }
        return c;
    }

    private Vector3 Alignement()
    {
        Vector3 friendsVelocity = Vector3.zero;
        List<Boid> inRange =
            boids.FindAll(b =>
                b != this
                && (b.transform.position - transform.position).sqrMagnitude > (avoidanceRadius * avoidanceRadius)
                && (b.transform.position - transform.position).sqrMagnitude < (magnetRadius * magnetRadius));
        foreach (Boid friend in inRange)
        {
            friendsVelocity += friend.velocity;
        }

        friendsVelocity /= (inRange.Count - 1);
        return (friendsVelocity - velocity) / 8;
    }

    private Vector3 Cohesion()
    {
        if (BoidMaster.Master.flockCohesion == Mode.Performant) 
            return (BoidMaster.flockCenter - transform.position)/100f;
        Vector3 flockCenter = Vector3.zero;
        foreach (Boid friend in boids)
        {
            if(friend == this) continue;
            flockCenter += friend.transform.position;
        }

        flockCenter /= (boids.Count - 1);
        return (flockCenter - transform.position) / 1000;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, avoidanceRadius);
    }
}
