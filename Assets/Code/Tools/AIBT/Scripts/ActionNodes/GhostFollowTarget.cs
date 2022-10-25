using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools.Trees.AI;

public class GhostFollowTarget : AIActionNode
{
   
    protected override void OnStart()
    {

    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        GameObject player;
        player = blackboard.player;
        agent.navMesh.SetDestination(player.transform.position + blackboard.targetOffset);
        return State.SUCC;
    }
}
