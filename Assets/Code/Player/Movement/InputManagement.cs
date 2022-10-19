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

    [Header("Drag")]
    [SerializeField] float groundDrag = 6f;
    [SerializeField] float airDrag = 3f;

    FPSControls inputs;
    Vector2 currentMove;
    Vector2 camDelta;
    float playerHeight = 2f;
    bool jumpPerform;
    bool isGrounded;
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
        inputs.Player.Jump.canceled += ctx =>
        {
            jumpPerform = ctx.ReadValueAsButton();
        };
    }

    private void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight / 2 + 0.1f);

        ControlDrag();
    }


    private void FixedUpdate()
    {
        MoveCharacter();
        
        if(jumpPerform && jumps < maxJumps && isGrounded)
        {
            CharacterJump();
        }

        if(isGrounded && jumps > 0)
        {
            jumps = 0;
        }
    }

    void ControlDrag()
    {
        if (isGrounded)
        {
            Rb.drag = groundDrag;
        }

        else
        {
            Rb.drag = airDrag;
        }
    }
    private void CharacterJump()
    {
        Debug.Log("JUmpu desu");
        Rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
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
