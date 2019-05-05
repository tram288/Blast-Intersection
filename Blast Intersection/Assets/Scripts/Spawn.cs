using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject enemy1;
    public GameObject enemy2;
    public float rate;
    float nextSpawn;
    int whatToSpawn;

    void Start()
    {
        
    }

    void Update()
    {
        if (Time.time > nextSpawn){
            whatToSpawn = Random.Range(1,3);
            switch(whatToSpawn){
                case 1:
                    Instantiate (enemy1, transform.position, Quaternion.identity);
                    break;
                 case 2:
                    Instantiate (enemy2, transform.position, Quaternion.identity);
                    break;
            }
        nextSpawn = Time.time + rate;
        }
    }
}
