using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RollPumpkin : MonoBehaviour
{
   NavMeshAgent NavMeshAgent;
    Rigidbody rb;

    private void Start()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        rb.AddTorque(NavMeshAgent.velocity);
    }
}
