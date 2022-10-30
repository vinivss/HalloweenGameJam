using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spin : MonoBehaviour
{

    public Vector3 torque;
    public Rigidbody rb;

    /*void Start()
    {
        rb = GetComponent<Rigidbody>();

        torque.x = Random.Range(-200, 200);
        torque.y = Random.Range(-200, 200);
        torque.z = Random.Range(-200, 200);
        rb.AddTorque(torque.x * torque.y * torque.z);
    }*/
}
