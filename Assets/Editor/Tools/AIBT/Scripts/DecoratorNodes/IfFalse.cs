using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools.Trees.AI;
public class IfFalse : AIDecoratorNode
{
    protected override void OnStart()
    {
        
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        child.Update();
        if(child.state == State.FAIL)
        {
            return State.SUCC;
        }

        return State.FAIL;
    }
}
