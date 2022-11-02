using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools.Trees.AI;

public class InstantiateThrowableObject : AIActionNode
{
    [SerializeField] int projectileSpeed;

    protected override void OnStart()
    {
        
    }

    protected override void OnStop()
    {
       
    }

    protected override State OnUpdate()
    {
        var throwObj = Instantiate(blackboard.objectToThrow, agent.throwPoint) as GameObject;
        throwObj.GetComponent<Rigidbody>().velocity = (blackboard.player.transform.position - agent.throwPoint.position).normalized * projectileSpeed;
        return State.SUCC;
    }
}
