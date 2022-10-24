using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//creates a wedge that will check whether or not the player has been seen
[ExecuteInEditMode]
public class AiSensor : MonoBehaviour
{
    public float distance = 10;
    public float angle = 30;
    public float height = 1.0f;
    public Color meshColr = Color.cyan;

    // how often scans will be run
    public int scanFrequency = 30;

    // which layers will be checked
    public LayerMask layers;

    //which layers block the detection
    public LayerMask OcclusionLayers;

    //buffer to store results from detections
    Collider[] colliders = new Collider[50];

    //get game objects that are actually withion the desired bounds
    public List<GameObject> Objects 
    {
        get
        {
            objects.RemoveAll(obj => !obj);
            return objects;
        }
    }

    private List<GameObject> objects = new List<GameObject>();
    // mesh that will represent senstor

    Mesh mesh;

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

        if(scanTimer < 0)
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
        for(int i = 0; i <count; i++)
        {
            GameObject obj = colliders[i].gameObject;

            if(InSight(obj))
            {
                objects.Add(obj);
            }
        }
    }

    public bool InSight(GameObject obj)
    {
        Vector3 origin = transform.position;
        Vector3 dest = obj.transform.position;
        Vector3 direction = dest - origin;


        //height check

        if (direction.y < 0 || direction.y > height)
        {
            return false;
        }

        direction.y = 0;
        float deltaAngle = Vector3.Angle(direction, transform.forward);

        //check whether the angle of the collider is within the bounds of the wedge angle
        if(deltaAngle > angle)
        {
            return false;
        }
        //making sure the origin is being originated from the center of the wedge
        origin.y += height / 2;
        dest.y = origin.y;

        //draw a line from the origin to the destination. if it finds something 
        if(Physics.Linecast(origin, dest, OcclusionLayers))
        {
            return false;
        }
        return true;
    }
    Mesh createWedgeMesh()
    {
        Mesh mesh = new Mesh();

        int segments = 10;
        int numTris = (segments * 4) +2 +2;
        int numVertices = numTris * 3;

        Vector3[] vertices = new Vector3[numVertices];

        int[] triangles = new int[numVertices];

        //define points opf the triangles
        Vector3 bottCenter = Vector3.zero;
        Vector3 bottLeft = Quaternion.Euler(0, -angle, 0) * Vector3.forward * distance;
        Vector3 bottRight = Quaternion.Euler(0, angle, 0) * Vector3.forward * distance;

        Vector3 topCenter = bottCenter + Vector3.up * height;
        Vector3 topLeft = bottLeft + Vector3.up * height; 
        Vector3 topRight = bottRight + Vector3.up * height;

        //index to vertices array
        int vert = 0;

        //left side 2 tris
        vertices[vert++] = bottCenter;
        vertices[vert++] = bottLeft;
        vertices[vert++] = topLeft;

        vertices[vert++] = topLeft;
        vertices[vert++] = topCenter;
        vertices[vert++] = bottCenter;

        //right side 2 tris
        vertices[vert++] = bottCenter;
        vertices[vert++] = topCenter;
        vertices[vert++] = topRight;

        vertices[vert++] = topRight;
        vertices[vert++] = bottRight;
        vertices[vert++] = bottCenter;

        // calculating angles of segments
        float currentAngle = -angle;
        float deltaAngle = (angle * 2) / segments;

        for(int i= 0; i < segments; ++i)
        {
             
             bottLeft = Quaternion.Euler(0, currentAngle, 0) * Vector3.forward * distance;
             bottRight = Quaternion.Euler(0, currentAngle + deltaAngle, 0) * Vector3.forward * distance;

            
             topLeft = bottLeft + Vector3.up * height;
             topRight = bottRight + Vector3.up * height;

            // far side 2 tris
            vertices[vert++] = bottLeft;
            vertices[vert++] = bottRight;
            vertices[vert++] = topRight;

            vertices[vert++] = topRight;
            vertices[vert++] = topLeft;
            vertices[vert++] = bottLeft;
            //top  1 tri
            vertices[vert++] = topCenter;
            vertices[vert++] = topLeft;
            vertices[vert++] = topRight;
            //bott 1 tri
            vertices[vert++] = bottCenter;
            vertices[vert++] = bottRight;
            vertices[vert++] = bottLeft;

            currentAngle += deltaAngle;
        }

        

        for(int i = 0; i <numVertices; i++)
        {
            triangles[i] = i;
        }

        mesh.vertices = vertices;

        mesh.triangles = triangles;

        mesh.RecalculateNormals();
        return mesh;
    }

    private void OnValidate()
    {
        mesh = createWedgeMesh();
        scanInterval = 1.0f / scanFrequency;
    }

    private void OnDrawGizmos()
    {
        if(mesh)
        {
            Gizmos.color = meshColr;
            Gizmos.DrawMesh(mesh, transform.position, transform.rotation);
        }

        Gizmos.DrawWireSphere(transform.position, distance);

        

        Gizmos.color = Color.green;
        foreach(var obj in Objects)
        {
            Gizmos.DrawSphere(obj.transform.position, 1.0f);
        }
    }

    public int Filter(GameObject[] buffer, string layerName)
    {
        int layer = LayerMask.NameToLayer(layerName);

        int count = 0;

        foreach(var obj in Objects)
        {
            if (obj.layer == layer)
            {
                buffer[count++] = obj;
            }

            if(buffer.Length == count)
            {
                break;
            }
        }

        return count;
    }
}
