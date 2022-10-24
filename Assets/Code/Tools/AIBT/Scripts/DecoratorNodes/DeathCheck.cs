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
        if (!blackboard.isAlive)
            return State.SUCC;
       return State.FAIL;
    }
}
