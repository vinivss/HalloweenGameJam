using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PlayerOverlapsphere : MonoBehaviour
{
    public float radius = 1;
    public float angle = 30;
    public float height = 1.0f;
    public Color meshColr = Color.red;
    public GameObject Player;

    bool checkpointAssigned;

    [HideInInspector] public Vector3 Offset;
    [HideInInspector] public Vector3 targetWaypoint;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    public Vector3 AssignCheckpointTransform()
    {
        Debug.Log("Called");
        Offset = (Random.insideUnitSphere) * radius;
        
        checkpointAssigned = true;

        Debug.Log(Offset);
        return Offset;
    }

    private void OnDrawGizmos()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Gizmos.color = meshColr;
         Gizmos.DrawWireSphere(Player.transform.position, radius);

        if(checkpointAssigned)
        {
            targetWaypoint = (Player.transform.position + Offset);
            Gizmos.DrawSphere(targetWaypoint, 0.3f);
        }
    }
}