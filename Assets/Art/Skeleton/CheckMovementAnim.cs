using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CheckMovementAnim : MonoBehaviour
{
    [SerializeField]Animator anim;
    NavMeshAgent agent;
    private void Awake()
    {
       anim = GetComponentInChildren<Animator>();
       agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(agent.velocity.x > 0 || agent.velocity.z >0)
        {
            anim.SetBool("IsMoving", true);
        }
    }
}
