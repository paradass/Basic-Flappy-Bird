using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    [SerializeField] private float speed;

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (Bird.Instance.isDead) return;
        transform.position += new Vector3(10 * Time.deltaTime, 0, 0);
        transform.position += new Vector3(Time.deltaTime * speed*-1, 0, 0);
    }
}
