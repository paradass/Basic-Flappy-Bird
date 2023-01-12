using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (Bird.Instance.isDead) return;
        transform.position += new Vector3(10 * Time.deltaTime, 0, 0);
    }
}
