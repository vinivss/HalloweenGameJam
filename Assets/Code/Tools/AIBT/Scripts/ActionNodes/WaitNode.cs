using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools.Trees.AI;


//wait for N seconds
public class WaitNode : AIActionNode
{
    [Min(0)] public float duration = 1;
    float startTime;

    protected override void OnStart()
    {
        startTime = Time.time;
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if(Time.time - startTime > duration)
        {
            return State.SUCC;
        }

        return State.RUN;
    }
}
