using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class InputManagement : MonoBehaviour
{
    [Header("Movement Values")]
    [Tooltip("basic movement Speed")]public float movespeed = 6f;
    [Tooltip("How strong is the Jump")] public float jumpStrength = 10f;
    [Tooltip("How many times can you Jump")] public int maxJumps = 1;

    [Header("Camera")]
    [SerializeField] Camera cam;

    FPSControls inputs;
    Vector2 currentMove;
    Vector2 camDelta;
    bool jumpPerform;
    int jumps = 0;

    Vector3 moveDir;

    Rigidbody Rb;
    private void Awake()
    {
       Rb = GetComponent<Rigidbody>();
       inputs = new FPSControls();

        inputs.Player.Movement.performed += ctx =>
        {
            currentMove = ctx.ReadValue<Vector2>();
            Debug.Log(currentMove);
        };

        inputs.Player.Movement.canceled += ctx =>
        {
            currentMove = Vector2.zero;
            
        };
        inputs.Player.Look.performed += ctx =>
        {
            camDelta = ctx.ReadValue<Vector2>();
        };

        inputs.Player.Jump.performed += ctx =>
        {
            jumpPerform = ctx.ReadValueAsButton();
        };
    }

    private void FixedUpdate()
    {
        MoveCharacter();
        
        if(jumpPerform && jumps < maxJumps)
        {
            CharacterJump();
        }
    }

    private void CharacterJump()
    {
        Rb.AddForce(0f, jumpStrength, 0f, ForceMode.Impulse);
    }

    private void MoveCharacter()
    {
        moveDir = cam.transform.forward * currentMove.y + cam.transform.right * currentMove.x;
        Rb.AddForce(moveDir.normalized * movespeed, ForceMode.Acceleration);
    }

    private void OnEnable()
    {
        inputs.Player.Enable(); 
    }

    private void OnDisable()
    {
        inputs.Player.Disable();
    }
}
