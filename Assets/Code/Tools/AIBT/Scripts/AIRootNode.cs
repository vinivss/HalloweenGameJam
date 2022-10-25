using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tools.Trees.AI
{
    public class AIRootNode : AINode
    {
        public AINode child;
        protected override void OnStart()
        {

        }

        protected override void OnStop()
        {

        }

        protected override State OnUpdate()
        {
            return child.Update();
        }

        public override AINode Clone()
        {
            AIRootNode node = Instantiate(this);   
            node.child = child.Clone(); 
            return node;
        }
    }
}
