using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float minY, maxY,spawnTime;
    [SerializeField] private GameObject pipe;
    [SerializeField] private GameObject tapToStart;
    bool isStarted;

    private void Update()
    {
        StartBird();
    }

    void StartBird()
    {
        if (Input.GetMouseButton(0) && !isStarted)
        {
            tapToStart.SetActive(false);
            Bird.Instance.StartBird();
            isStarted = true;
            InvokeRepeating("SpawnPipe", 0, spawnTime);
        }
    }

    void SpawnPipe()
    {
        Instantiate(pipe, new Vector3(Bird.Instance.transform.position.x+5, Random.Range(minY, maxY), 0), Quaternion.identity);
    }
}
