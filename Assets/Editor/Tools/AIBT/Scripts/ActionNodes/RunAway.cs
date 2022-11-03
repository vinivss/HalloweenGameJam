using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools.Trees.AI;

public class RunAway : AIActionNode
{
    public float RunAwayDist = 10.0f;
    protected override void OnStart()
    {
        
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        Vector3 playerPos = blackboard.player.transform.position;
        float dist = Vector3.Distance(agent.transform.position, playerPos);


        if (dist < RunAwayDist)
        {
            Debug.Log("Run away");
            Vector3 dirToPlayer = agent.transform.position - playerPos;

            Vector3 newPos = agent.transform.position + dirToPlayer;
            agent.navMesh.SetDestination(newPos);

        }

        else if (dist >= RunAwayDist)
        {
            
            return State.SUCC;
        }

        return State.RUN;
    }
}
