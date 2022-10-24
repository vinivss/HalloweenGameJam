using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class AggroRange : MonoBehaviour
{
    public float distance = 10;
    public float angle = 30;
    public float height = 1.0f;
    public Color meshColr = Color.red;

    // how often scans will be run
    public int scanFrequency = 30;

    // which layers will be checked
    public LayerMask layers;

    //buffer to store results from detections
    Collider[] colliders = new Collider[50];

    public List<GameObject> Objects
    {
        get
        {
            objects.RemoveAll(obj => !obj);
            return objects;
        }
    }

    private List<GameObject> objects = new List<GameObject>();

    int count;

    float scanInterval;
    float scanTimer;


    // Start is called before the first frame update
    void Start()
    {
        scanInterval = 1.0f / scanFrequency;
    }

    // Update is called once per frame
    void Update()
    {
        scanTimer -= Time.deltaTime;

        if (scanTimer < 0)
        {
            scanTimer += scanInterval;
            Scan();
        }
    }

    private void Scan()
    {
        //get all of the colliders around the player
        count = Physics.OverlapSphereNonAlloc(transform.position, distance, colliders, layers, QueryTriggerInteraction.Collide);

        objects.Clear();

        //if it is within the visible area just check and add each variable into the List of in sigh objects
        for (int i = 0; i < count; i++)
        {
            GameObject obj = colliders[i].gameObject;
            objects.Add(obj);
        }
    }
    private void OnValidate()
    {
        
        scanInterval = 1.0f / scanFrequency;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = meshColr;
        Gizmos.DrawWireSphere(transform.position, distance);


        // drawSpheres on the colliders that are crrently overlapping
        for (int i = 0; i < count; ++i)
        {
            Gizmos.DrawSphere((colliders[i].transform.position + new Vector3(0,2.0f,0)), 1.0f);
        }
    }

}
