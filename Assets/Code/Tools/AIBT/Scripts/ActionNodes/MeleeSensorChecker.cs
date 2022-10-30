using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools.Trees.AI;

public class MeleeSensorChecker : AIActionNode
{
    protected override void OnStart()
    {
        
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        if(agent.MsensorRange.Objects.Count > 0)
        {
            return State.SUCC;
        }
        return State.FAIL;
    }
}
