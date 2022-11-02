using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools.Trees.AI;

public class InstantiateThrowableObject : AIActionNode
{
    [SerializeField] int projectileSpeed;
    [SerializeField]float waitTime = 2;
    float startTime;
  
    protected override void OnStart()
    {

        startTime = Time.time;
    }

    protected override void OnStop()
    {
       
    }

    protected override State OnUpdate()
    {
        var throwObj = Instantiate(blackboard.objectToThrow, agent.throwPoint) as GameObject;
        if (Time.time - startTime > waitTime)
        {
            throwObj.GetComponent<Rigidbody>().velocity = (blackboard.player.transform.position - agent.throwPoint.position).normalized * projectileSpeed;
            return State.SUCC;
        }
        return State.RUN;
    }
}
