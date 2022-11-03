using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools.Trees.AI;
public class GetTransformInSphere : AIActionNode
{
   
    protected override void OnStart()
    {
        
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        blackboard.targetOffset = agent.playerOverlap.AssignCheckpointTransform();
     

        return State.SUCC;
    }
}
