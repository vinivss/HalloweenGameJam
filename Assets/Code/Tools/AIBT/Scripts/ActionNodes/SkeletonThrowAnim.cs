using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools.Trees.AI;

public class SkeletonThrowAnim : AIActionNode
{
    protected override void OnStart()
    {
        
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        agent.animator.SetBool("Shoot", true);
        return State.SUCC;
    }
}
