using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools.Trees.AI;
public class RangedSensorChecker : AIActionNode
{
    protected override void OnStart()
    {
       
    }

    protected override void OnStop()
    {
       
    }

    protected override State OnUpdate()
    {
        if (agent.RsensorRange.Objects.Count > 0)
        {
            return State.SUCC;
        }
        return State.FAIL;
    }
}
