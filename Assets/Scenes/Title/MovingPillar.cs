using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPillar : MonoBehaviour
{
    public float speed = 1f;
    public float rotSpeed = 1f;

    void FixedUpdate() {
        this.transform.Translate(-Vector3.right * Time.deltaTime * speed);
        transform.RotateAround(transform.position, transform.up, Time.deltaTime * rotSpeed);
    }
}
