using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools.Trees.AI;


public class SelectRandomChild : AICompositeNode
{
    protected override void OnStart()
    {
       
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        int RandomIndex = Random.Range(0,this.children.Count);

        var childToPick = children[RandomIndex];

        childToPick.Update();

        return childToPick.state;
    }
}
