using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools.Trees.AI;


public class DeathCheck : AIActionNode
{
    protected override void OnStart()
    {
 
    }

    protected override void OnStop()
    {
       
    }

    protected override State OnUpdate()
    {
        if (agent.currentHealth <= 0)
        {
            blackboard.isAlive = false;
            return State.SUCC;
        }
       return State.FAIL;
    }
}
