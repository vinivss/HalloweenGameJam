using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Tools.Trees.AI
{
  
    [CreateAssetMenu(fileName = "AI Behaviour Tree", menuName = "Tools/AI/new AI Behaviour Tree", order = 3)]
    public class AIBehaviourTree : ScriptableObject
    {
        public AINode rootNode;
        public AINode.State treeState = AINode.State.RUN;
        public List<AINode> nodes = new List<AINode>();
        public AIBlackBoard board = new AIBlackBoard();


        public AINode.State Update()
        {
            if (rootNode.state == AINode.State.RUN)
            {
                return rootNode.Update();
            }

            return treeState;
        }
#if UNITY_EDITOR
        public AINode CreateNode(System.Type type)
        {
            //create a new node and add it to sub list
            AINode node = ScriptableObject.CreateInstance(type) as AINode;
            node.name = type.Name;
            node.GUID = GUID.Generate().ToString();
            Undo.RecordObject(this, "AIBT Create Node");
            nodes.Add(node);

            if (!Application.isPlaying)
            {
                AssetDatabase.AddObjectToAsset(node, this);
            }
            Undo.RegisterCreatedObjectUndo(node, "AIBT Create Node");
            AssetDatabase.SaveAssets();
            return node;
        }

        public void DeleteNode(AINode node)
        {
            Undo.RecordObject(this, "AIBT Delete Node");
            nodes.Remove(node);
            Undo.DestroyObjectImmediate(node);
            AssetDatabase.SaveAssets();
        }

        public void AddChild(AINode parent, AINode child)
        {
            AIRootNode rootNode = parent as AIRootNode;
            if (rootNode)
            {
                Undo.RecordObject(rootNode, "AIBT Add Child");
                rootNode.child = child;
                EditorUtility.SetDirty(rootNode);
            }

            AIDecoratorNode decorator = parent as AIDecoratorNode;
            if(decorator)
            {
                Undo.RecordObject(decorator, "AIBT Add Child");
                decorator.child = child;
                EditorUtility.SetDirty(decorator);
            }

            AICompositeNode composite = parent as AICompositeNode;
            if(composite)
            {
                Undo.RecordObject(composite, "AIBT Add Child");
                composite.children.Add(child);
                EditorUtility.SetDirty(composite);
            }
        }
        public void KillChild(AINode parent, AINode child)
        {
            AIRootNode rootNode = parent as AIRootNode;
            if (rootNode)
            {
                Undo.RecordObject(rootNode, "AIBT Kill Child");
                rootNode.child = null;
                EditorUtility.SetDirty(rootNode);
            }

            AIDecoratorNode decorator = parent as AIDecoratorNode;
            if (decorator)
            {
                Undo.RecordObject(decorator, "AIBT Kill Child");
                decorator.child = null;
                EditorUtility.SetDirty(decorator);
            }

            AICompositeNode composite = parent as AICompositeNode;
            if (composite)
            {
                Undo.RecordObject(composite, "AIBT Kill Child");
                composite.children.Remove(child);
                EditorUtility.SetDirty(composite);
            }
        }
        public List<AINode> GetChildren(AINode parent)
        {
            List<AINode> children = new List<AINode> ();

            AIRootNode rootNode = parent as AIRootNode;
            if (rootNode && rootNode.child != null)
            {
                children.Add(rootNode.child);
            }

            AIDecoratorNode decorator = parent as AIDecoratorNode;
            if(decorator && decorator.child != null)
            {
                children.Add(decorator.child);
            }

            AICompositeNode composite = parent as AICompositeNode;
            if(composite)
            {
                return composite.children;
            }

            return children;
        }
#endif
        public void Traverse(AINode node, System.Action<AINode> visitor)
        {
            if(node)
            {
                visitor.Invoke(node);
                var children = GetChildren(node);
                children.ForEach((n) => Traverse(n,visitor));
            }
        }
        public AIBehaviourTree Clone()
        {
            AIBehaviourTree tree = Instantiate(this);
            tree.rootNode = tree.rootNode.Clone();
            tree.nodes = new List<AINode>();
            Traverse(tree.rootNode, (n) =>
            {
                tree.nodes.Add(n);
            });
            return tree;
        }

        public void Bind(AIAgent agent)
        {
            Traverse(rootNode, node =>
            {
                node.agent = agent;
                node.blackboard = board;
            });
        }
    }
}
