using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tools.Trees.AI
{
    //blueprint for nodes that do an action in the Behaviour
    public abstract class AICompositeNode : AINode
    {
       [HideInInspector]public List<AINode> children = new List<AINode>();

        public override AINode Clone()
        {
            AICompositeNode node = Instantiate(this);
            node.children = children.ConvertAll(c=> c.Clone());
            return node;
        }
    }
}
