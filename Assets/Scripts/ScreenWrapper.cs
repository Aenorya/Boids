using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrapper : MonoBehaviour
{
    [SerializeField] private Vector2 spawnArea, spriteBounds;
    [SerializeField] private float margin;

    // Start is called before the first frame update
    void Awake()
    {
        spawnArea = Camera.main.ViewportToWorldPoint(Vector3.one) - Camera.main.ViewportToWorldPoint(Vector3.zero);
        spriteBounds = GetComponent<SpriteRenderer>().bounds.size;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x + margin < -spawnArea.x / 2)
        {
            transform.position = new Vector3(spawnArea.x / 2, transform.position.y, transform.position.z);
        }else if (transform.position.x - margin > spawnArea.x / 2)
        {
            transform.position = new Vector3(-spawnArea.x / 2, transform.position.y, transform.position.z);
        }
        
        if (transform.position.y  < -spawnArea.y /2 -margin)
        {
            transform.position = new Vector3( transform.position.x,spawnArea.y / 2, transform.position.z);
        }else if (transform.position.y > spawnArea.y/2 + margin )
        {
            transform.position = new Vector3(transform.position.x,-spawnArea.y / 2,  transform.position.z);
        }
    }
}
