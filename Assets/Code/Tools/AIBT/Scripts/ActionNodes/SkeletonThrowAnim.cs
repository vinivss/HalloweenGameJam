using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools.Trees.AI;
using UnityEditor.Experimental.RestService;

public class SkeletonThrowAnim : AIActionNode
{
    bool playedOnce;
    protected override void OnStart()
    {
        
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        agent.animator.SetBool("Shoot", true);

        if (agent.animator.GetBool("Shoot") == true && playedOnce == true)
        {
            playedOnce = false;
            agent.animator.SetBool("Shoot", false);
            return State.SUCC;
        }

        playedOnce = true;
        return State.RUN;
    }


}
