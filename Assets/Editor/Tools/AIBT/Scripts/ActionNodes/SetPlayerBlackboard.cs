using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools.Trees.AI;

public class SetPlayerBlackboard : AIActionNode
{
    protected override void OnStart()
    {
        
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        blackboard.player = agent.FindPlayerinScene();
        return State.SUCC;
    }
}
