using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tools.Trees.AI
{
    //blueprint of Decorator nodes 
    public abstract class AIDecoratorNode : AINode
    {
        [HideInInspector]public AINode child;

        public override AINode Clone()
        {
            AIDecoratorNode node = Instantiate(this);
            node.child = child.Clone();
            return node;
        }
    }
}
