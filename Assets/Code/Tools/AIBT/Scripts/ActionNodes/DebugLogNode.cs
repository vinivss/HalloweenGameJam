using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools.Trees.AI;

public class DebugLogNode : AIActionNode
{
    public string msg;
    protected override void OnStart()
    {

    }

    protected override void OnStop()
    {
        Debug.Log("End of Message");
    }

    protected override State OnUpdate()
    {
        Debug.Log(msg);

        return State.SUCC;
    }
}
