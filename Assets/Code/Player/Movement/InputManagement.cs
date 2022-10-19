using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InputManagement : MonoBehaviour
{
    [Header("Movement Values")]
    [Tooltip("basic movement Speed")]
    public float movespeed = 6f;
    float movementMultiplier = 10f;
    [Tooltip("How strong is the Jump")]
    public float jumpStrength = 10f;
    [Tooltip("How many times can you Jump")]
    public int maxJumps = 1;
    [Tooltip("How fast you move while in the air")]
    [SerializeField] float airMultiplier = 10f;

    [Header("Camera")]
    [SerializeField] Camera cam;

    [Header("Drag")]
    [SerializeField] float groundDrag = 6f;
    [SerializeField] float airDrag = 3f;

    [Header("Floor detection")]
    [Tooltip("Center of Sphere for ground Detection")]
    [SerializeField] Transform groundCheckPos;
    [Min(0)][Tooltip("Radius of Sphere used in ground detection")]
    [SerializeField] float groundDistance = 0.4f;
    [Tooltip("layer for ground to be detected")]
    [SerializeField] LayerMask groundMask;

    FPSControls inputs;
    Vector2 currentMove;
    Vector2 camDelta;
    float playerHeight = 2f;
    [HideInInspector]public bool jumpPerform;
    bool isGrounded;
    int jumps = 0;

    Vector3 moveDir;
    Vector3 slopeMoveDir;

    RaycastHit slopeHit;

    Rigidbody Rb;
    private void Awake()
    {
       Rb = GetComponent<Rigidbody>();
       inputs = new FPSControls();

        inputs.Player.Movement.performed += ctx =>
        {
            currentMove = ctx.ReadValue<Vector2>();
           // Debug.Log(currentMove);
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheckPos.position, groundDistance);
    }
    private void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheckPos.position, groundDistance, groundMask);

        ControlDrag();

        slopeMoveDir = Vector3.ProjectOnPlane(moveDir, slopeHit.normal);
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
            Rb.velocity = new(Rb.velocity.x, 0, Rb.velocity.z);
            Rb.drag = groundDrag;
        }

        else
        {
            Rb.drag = airDrag;
        }
    }

    private bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 0.5f))
        {
            if(slopeHit.normal != Vector3.up)
            {
                return true;
            }
        }
        return false;
    }
    private void CharacterJump()
    {
       // Debug.Log("JUmpu desu");
        Rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
    }

    private void MoveCharacter()
    {
        moveDir = cam.transform.forward * currentMove.y + cam.transform.right * currentMove.x;
        moveDir.y = 0;
        if (isGrounded && !OnSlope())
        {
            Rb.AddForce(moveDir.normalized * movespeed * movementMultiplier, ForceMode.Acceleration);
        }

        else if (isGrounded && OnSlope())
        {
            Rb.AddForce(slopeMoveDir.normalized * movespeed * movementMultiplier, ForceMode.Acceleration);
        }

        else if(!isGrounded)
        {
            Rb.AddForce(moveDir.normalized * movespeed * airMultiplier * movementMultiplier, ForceMode.Acceleration);
        }
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
