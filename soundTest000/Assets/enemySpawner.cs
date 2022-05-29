using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    public GameObject enemy;

    void Start()
    {
        InvokeRepeating("Spawn",1,Random.Range(0.2f,1));
    }

    void Spawn()
    {
        GameObject spawnedBall = Instantiate(enemy, gameObject.transform.position + new Vector3(0, 0, Random.Range(0f, 5f)), Quaternion.identity);
    }
}
