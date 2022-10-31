using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CheckMovementAnim : MonoBehaviour
{
    Animator anim;
    Rigidbody rb;
    private void Awake()
    {
       anim = GetComponent<Animator>();
       rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.velocity.x > 0.1f)
        {
            anim.SetBool("isMoving", true);
        }
    }
}
