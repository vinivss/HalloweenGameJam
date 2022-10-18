using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using Tools.Trees.AI;
using System;
using System.Linq;

//graph view for UXML
public class AIBehaviourTreeView : GraphView
{
    public Action<AINodeView> OnNodeSelected;
    public new class UxmlFactory : UxmlFactory<AIBehaviourTreeView, GraphView.UxmlTraits> { };
    AIBehaviourTree tree;



    public AIBehaviourTreeView()
    {
        // adding stylesheet view 
        Insert(0, new GridBackground());//making the grid background

        //manipulators for dragging and selecting all the different things
        this.AddManipulator(new ContentZoomer());
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Code/Tools/AIBT/Editor/AIBehaviourTreeEditor.uss");
        styleSheets.Add(styleSheet);

        Undo.undoRedoPerformed += OnUndoRedo;
    }

    private void OnUndoRedo()
    {
        PopulateView(tree);
        AssetDatabase.SaveAssets();
    }

    AINodeView findNodeView(Tools.Trees.AI.AINode node)
    {
        return GetNodeByGuid(node.GUID) as AINodeView;
    }
    internal void PopulateView(AIBehaviourTree tree)
    {
        this.tree = tree;

        graphViewChanged -= OnGraphViewChanged;
        DeleteElements(graphElements);
        graphViewChanged += OnGraphViewChanged;

        if (tree.rootNode == null)
        {
            tree.rootNode = tree.CreateNode(typeof(AIRootNode)) as AIRootNode;
            EditorUtility.SetDirty(tree);
            AssetDatabase.SaveAssets();
        }
        //creates node views
        tree.nodes.ForEach(n => CreateNodeView(n));

        //create edges
        tree.nodes.ForEach(n =>
        {
            var children = tree.GetChildren(n);
            children.ForEach(c =>
            {
                AINodeView parentView = findNodeView(n);
                AINodeView childView = findNodeView(c);

                Edge edge = parentView.output.ConnectTo(childView.input);
                AddElement(edge);
            });
        });

    }
    private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
    {
        if(graphViewChange.elementsToRemove != null)
        {
            graphViewChange.elementsToRemove.ForEach(elem =>
            {
                AINodeView nodeView = elem as AINodeView;
                if (nodeView != null)
                {
                    tree.DeleteNode(nodeView.node);
                }

                Edge edge = elem as Edge;
                if(edge != null)
                {
                    AINodeView parentView = edge.output.node as AINodeView;
                    AINodeView childView = edge.input.node as AINodeView;
                    tree.KillChild(parentView.node, childView.node);
                }
            });
        }
        //making sure that the edge creation actually makes it a child
        if(graphViewChange.edgesToCreate != null)
        {
            graphViewChange.edgesToCreate.ForEach(edge =>
            {
                AINodeView parentView = edge.output.node as AINodeView;
                AINodeView childView = edge.input.node as AINodeView;
                tree.AddChild(parentView.node, childView.node);
            });
        }

        if(graphViewChange.movedElements != null)
        {
            nodes.ForEach((n) =>
            {
                AINodeView view = n as AINodeView;
                view.SortChildren();
            });
        }
        return graphViewChange;
    }
    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        return ports.ToList().Where(endPort =>
        endPort.direction != startPort.direction && 
        endPort.node != startPort.node).ToList();
    }
    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {
        //base.BuildContextualMenu(evt);
        {
            var types = TypeCache.GetTypesDerivedFrom<AIActionNode>();
            foreach (var type in types)
            {
                evt.menu.AppendAction($"[ActionNode]/  {type.Name}", (a) => CreateNode(type));
            }
        }
        {
            var types = TypeCache.GetTypesDerivedFrom<AICompositeNode>();
            foreach (var type in types)
            {
                evt.menu.AppendAction($"[Composite Node]/ {type.Name}", (a) => CreateNode(type));
            }
        }
        {
            var types = TypeCache.GetTypesDerivedFrom<AIDecoratorNode>();
            foreach (var type in types)
            {
                evt.menu.AppendAction($"[Decorator Node]/ {type.Name}", (a) => CreateNode(type));
            }
        }
    }
    //actual visual elements for node
    void CreateNode(System.Type type)
    {
       Tools.Trees.AI.AINode node =  tree.CreateNode(type);
       CreateNodeView(node);
    }
    void CreateNodeView(Tools.Trees.AI.AINode node)
    {
        AINodeView nodeView = new AINodeView(node);
        nodeView.OnNodeSelected = OnNodeSelected;
        AddElement(nodeView);
    }

    public void UpdateNodeState()
    {
        nodes.ForEach((n) =>
        {
            AINodeView view = n as AINodeView;
            view.UpdateState();
        });
    }
}
