using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class WallRun : MonoBehaviour
{
    [Header("Movement Reference")]
    [SerializeField] Transform orientation;

    [Header("Detection")]
    [Tooltip("How far away from the wall do you need to be")]
    [SerializeField] float wallDistance = 0.5f;
    [Tooltip("Minimum jump height before attaching to wall")]
    [SerializeField] float minimumJumpHeight = 1.5f;
    [Tooltip("layers to be checked for wall collision")]
    [SerializeField] LayerMask WallChecks;

    [Header("Movement")]
    [Tooltip("How fast do you slide down a wall")]
    [SerializeField] float wallGravity;
    [Tooltip("How Strong the Wall jump is")]
    [SerializeField] float wallJumpStrength;

    [Header("Camera")]
    [Tooltip("Camera to apply Effects to")]
    [SerializeField] CinemachineVirtualCamera cam;
    [Tooltip("How much to Dutch Angle")]
    [SerializeField] float dutchAngle = 4.0f;

    bool wallLeft = false;
    bool wallRight = false;

    RaycastHit leftWallHit;
    RaycastHit rightWallHit;

    Rigidbody rb;

    InputManagement input;


    private void Start()
    {
        input = GetComponent<InputManagement>();   
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        CheckWall();
        if(CanWallRun())
        {
            if(wallLeft || wallRight)
            {
                StartWallRun();
                
            } 

            else
            {
                EndWallRun();
            }
        }

    }

    void CheckWall()
    {
        Debug.DrawRay(transform.position, -orientation.right * wallDistance, Color.green) ;
        wallLeft = Physics.Raycast(transform.position, -orientation.right,out leftWallHit, wallDistance, WallChecks );

        Debug.DrawRay(transform.position, orientation.right * wallDistance, Color.green);
        wallRight = Physics.Raycast(transform.position, orientation.right,out rightWallHit, wallDistance, WallChecks);
    }

    bool CanWallRun()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minimumJumpHeight);
    }

    void StartWallRun()
    {
        rb.useGravity = false;
        rb.AddForce(Vector3.down * wallGravity, ForceMode.Force);
        if(wallLeft)
        {
            //Debug.Log("left Dutch");
            cam.m_Lens.Dutch = dutchAngle * -1.0f;
        }

        else if(wallRight)
        {
            cam.m_Lens.Dutch = dutchAngle;
        }
        if (input.jumpPerform)
        {
            if (wallLeft)
            {
                Debug.Log("left Wall Run");
                Vector3 wallJumpDir = transform.up + leftWallHit.normal;
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(wallJumpDir * wallJumpStrength * 100, ForceMode.Force);
                cam.m_Lens.Dutch = 0.0f;
            }

            else if (wallRight)
            {
                Vector3 wallJumpDir = transform.up + rightWallHit.normal;
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(wallJumpDir * wallJumpStrength * 100, ForceMode.Force);
                cam.m_Lens.Dutch = 0.0f;
            }
        }
    }

    void EndWallRun()
    {
        rb.useGravity = true;
        cam.m_Lens.Dutch = 0.0f;

    }
}
