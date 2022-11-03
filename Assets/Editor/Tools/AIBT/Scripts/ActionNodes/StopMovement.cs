using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools.Trees.AI;
public class StopMovement : AIActionNode
{
    protected override void OnStart()
    {
       
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        agent.navMesh.ResetPath();
        return State.SUCC;
    }
}
