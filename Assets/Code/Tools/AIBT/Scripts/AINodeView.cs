using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System;
using UnityEditor;

namespace Tools.Trees.AI
{
    public class AINodeView : UnityEditor.Experimental.GraphView.Node
    {
        public Action<AINodeView> OnNodeSelected;
        public AINode node;
        public Port input;
        public Port output;

        public AINodeView(AINode node): base ("Assets/Code/Tools/AIBT/Editor/AINodeView.uxml")
        {
            this.node = node;
            this.title = node.name;
            this.viewDataKey = node.GUID;

            //setting pos of key
            style.left = node.position.x;
            style.top = node.position.y;

            CreateInputPorts();
            CreateOutputPorts();
            SetupClasses();

            Label descriptionLabel = this.Q<Label>("Description");
            descriptionLabel.bindingPath = "Description";
            descriptionLabel.Bind(new SerializedObject(node));
        }

        private void SetupClasses()
        {
            if(node is AIRootNode)
            {
                AddToClassList("root");
            }
            else if(node is AIActionNode)
            {
                AddToClassList("action");
            }
            else if(node is AIDecoratorNode)
            {
                AddToClassList("decorator");
            }
            else if(node is AICompositeNode)
            {
                AddToClassList("composite");
            }
        }

        private void CreateOutputPorts()
        {
            if (node is AIRootNode)
            {
                output = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single, typeof(bool));
            }
            else if (node is AIActionNode)
            {

            }
            else if(node is AICompositeNode)
            {
                output = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Multi, typeof(bool));
            }
            else if(node is AIDecoratorNode)
            {
                output = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single, typeof(bool));
            }
            if (output != null)
            {
                output.portName = "";
                output.style.flexDirection = FlexDirection.ColumnReverse;
                output.portColor = Color.blue;
                outputContainer.Add(output);
            }
        }

        private void CreateInputPorts()
        {
            //creating all input nodes for the different types and how many children they accept
            if(node is AIRootNode)
            {

            }
            else if (node is AIActionNode)
            {
                input = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
            }
            else if (node is AICompositeNode)
            {
                input = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
            }
            else if (node is AIDecoratorNode)
            {
                input = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
            }

            if(input != null)
            {
                input.portName = "";
                input.style.flexDirection = FlexDirection.Column;
                input.portColor = Color.red;
                inputContainer.Add(input);
            }
        }
    

        public override void SetPosition(Rect newPos)
        {
            base.SetPosition(newPos);
            Undo.RecordObject(node, "AIBT Set Pos");
            node.position.x = newPos.xMin;
            node.position.y = newPos.yMin;
            EditorUtility.SetDirty(node);
        }
        public override void OnSelected()
        {
            base.OnSelected();
            if (OnNodeSelected != null)
            {
                OnNodeSelected.Invoke(this);
            }
         }
        public void SortChildren()
        {
            AICompositeNode composite = node as AICompositeNode;

            if(composite)
            {
                composite.children.Sort(SortByHorizontalPosition);
            }
        }

        private int SortByHorizontalPosition(AINode left, AINode right)
        {
            return left.position.x < right.position.x ? -1 : 1;
        }

        public void UpdateState()
        {
            RemoveFromClassList("running");
            RemoveFromClassList("fail");
            RemoveFromClassList("success");
            if (Application.isPlaying)
            {
                switch (node.state)
                {
                    case AINode.State.RUN:
                        if (node.started)
                        {
                            AddToClassList("running");
                        }
                        break;
                    case AINode.State.FAIL:
                        AddToClassList("fail");
                        break;
                    case AINode.State.SUCC:
                        AddToClassList("success");
                        break;
                }
            }
        }
    }
}
