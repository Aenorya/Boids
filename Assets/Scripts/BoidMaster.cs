using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoidMaster : MonoBehaviour
{
    public static List<BoidBehaviour> boids = new List<BoidBehaviour>();
    [SerializeField] private BoidBehaviour boidPrefab;
    [SerializeField] private int boidsCount;
    [SerializeField] private Vector2 spawnArea;

    void Start()
    {
        spawnArea = Camera.main.ViewportToWorldPoint(Vector3.one) - Camera.main.ViewportToWorldPoint(Vector3.zero);
        //spawnArea = new Vector2(Screen.width, Screen.height);
        float randX, randY, randR;
        for (int b = 0; b < boidsCount; b++)
        {
            randX = Random.Range(0, spawnArea.x) - spawnArea.x / 2;
            randY = Random.Range(0, spawnArea.y) - spawnArea.y / 2;
            randR = Random.Range(-180, 180);
            BoidBehaviour boid = Instantiate(boidPrefab,
                new Vector3(randX, randY, 0),
                Quaternion.Euler(0, 0,randR));
                //Quaternion.identity);
            boids.Add(boid);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(Vector3.zero, spawnArea);
    }
}
