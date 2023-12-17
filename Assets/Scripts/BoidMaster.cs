using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoidMaster : MonoBehaviour
{
    public static BoidMaster Master;
    public static List<Boid> boids = new List<Boid>();
    public static Vector3 flockCenter;
    public  Mode flockCohesion = Mode.Performant;

    [SerializeField] private Boid boidPrefab;
    [SerializeField] private int boidsCount;
    [SerializeField] private Vector2 spawnArea;

    void Start()
    {
        Master = this;
        spawnArea = Camera.main.ViewportToWorldPoint(Vector3.one) - Camera.main.ViewportToWorldPoint(Vector3.zero);
        //spawnArea = new Vector2(Screen.width, Screen.height);
        float randX, randY, randR;
        for (int b = 0; b < boidsCount; b++)
        {
            randX = Random.Range(0, spawnArea.x) - spawnArea.x / 2;
            randY = Random.Range(0, spawnArea.y) - spawnArea.y / 2;
            randR = Random.Range(-90, 90);
            Boid boid = Instantiate(boidPrefab,
                new Vector3(randX, randY, 0),
                Quaternion.Euler(0, 0,randR));
            boids.Add(boid);
        }
    }

    private void Update()
    {
        if (flockCohesion == Mode.Precise) return;
        flockCenter = Vector3.zero;
        foreach (Boid friend in boids)
        {
            flockCenter += friend.transform.position;
        }

        flockCenter /= (boids.Count - 1);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(Vector3.zero, spawnArea);
    }
}

public enum Mode
{
    Performant,
    Precise
}