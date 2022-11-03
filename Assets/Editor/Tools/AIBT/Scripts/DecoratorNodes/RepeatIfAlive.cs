using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools.Trees.AI;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;

public class RepeatIfAlive : AIDecoratorNode
{
    
    protected override void OnStart()
    {
        
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        if(blackboard.isAlive == false)
        {
            return State.SUCC;
        }

        child.Update();
        return State.RUN;
    }
}
