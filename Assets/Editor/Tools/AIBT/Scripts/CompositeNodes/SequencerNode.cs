using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools.Trees.AI;


//If statement equivalent
public class SequencerNode : AICompositeNode
{
    int curr;

    protected override void OnStart()
    {
        curr = 0;
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        var child = children[curr];
        switch(child.Update())
        {
            case State.RUN:
                return State.RUN;
            case State.FAIL:
                return State.FAIL;
            case State.SUCC:
                curr++;
                break;
        }

        return curr == children.Count ? State.SUCC : State.RUN;
    }
}
