using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    private Vector3 avoidance, alignment, cohesion;
    private Vector3 velocity;
    [SerializeField] private float speed = 1f;

    private List<Boid> boids = new List<Boid>();
    void Start()
    {
        boids = BoidMaster.boids;
    }

    // Update is called once per frame
    void Update()
    {
        cohesion = Cohesion();
        velocity = avoidance + alignment + cohesion;
        RotateTowardsDirection();        
        transform.position += velocity*(Time.deltaTime*speed);
    }

    private void RotateTowardsDirection()
    {
        float angle = Vector3.Angle(transform.up, velocity);
        transform.Rotate(0,0,angle);
    }
    private Vector3 Avoidance()
    {
        return Vector3.zero;
    }

    private Vector3 Alignement()
    {
        return Vector3.zero;
    }

    private Vector3 Cohesion()
    {
        if (BoidMaster.Master.flockCohesion == Mode.Optimized) 
            return (BoidMaster.flockCenter - transform.position)/100f;
        Vector3 flockCenter = Vector3.zero;
        foreach (Boid friend in boids)
        {
            if(friend == this) continue;
            flockCenter += friend.transform.position;
        }

        flockCenter /= (boids.Count - 1);
        return (flockCenter - transform.position) / 100;
    }
}
