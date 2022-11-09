using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float minY, maxY,spawnTime;
    [SerializeField] private GameObject pipe;
    void Start()
    {
        InvokeRepeating("SpawnPipe", 0, spawnTime);
    }

    void SpawnPipe()
    {
        Instantiate(pipe, new Vector3(5, Random.Range(minY, maxY), 0), Quaternion.identity);
    }
}
