using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Callbacks;
using Tools.Trees.AI;
using System;

public class AIBehaviourTreeEditor : EditorWindow
{
    AIBehaviourTreeView treeView;
    AIInspectorView inspectorView;
    IMGUIContainer blackboardView;

    SerializedObject treeObject;
    SerializedProperty blackboardProperty;

    [MenuItem("Tools/AI/Editor ...")]
    public static void OpenWindow()
    {
        AIBehaviourTreeEditor wnd = GetWindow<AIBehaviourTreeEditor>();
        wnd.titleContent = new GUIContent("AIBehaviourTreeEditor");
    }

    [OnOpenAsset]
    public static bool OnOpenAsset(int instanceId, int line)
    {
        if(Selection.activeObject is AIBehaviourTree)
        {
            OpenWindow();
            return true;
        }
        return false;
    }
    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Code/Tools/AIBT/Editor/AIBehaviourTreeEditor.uxml");
        visualTree.CloneTree(root);
       

        // A stylesheet can be added to a VisualElement.
        // The style will be applied to the VisualElement and all of its children.
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Code/Tools/AIBT/Editor/AIBehaviourTreeEditor.uss");
        root.styleSheets.Add(styleSheet);

        treeView = root.Q<AIBehaviourTreeView>();
        inspectorView = root.Q<AIInspectorView>();
        blackboardView = root.Q<IMGUIContainer>();
       
            blackboardView.onGUIHandler = () =>
            {

                
                treeObject.Update();
                EditorGUILayout.PropertyField(blackboardProperty);
                treeObject.ApplyModifiedProperties();
            };
        
        treeView.OnNodeSelected = OnNodeSelectionChanged;
        OnSelectionChange();
    }
    private void OnEnable()
    {
        EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }


    private void OnDisable()
    {
        EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
    }

    private void OnPlayModeStateChanged(PlayModeStateChange obj)
    {
        switch(obj)
        {
            case PlayModeStateChange.EnteredEditMode:
                OnSelectionChange();
                break;
            case PlayModeStateChange.ExitingEditMode:
                //OnSelectionChange();
                break;
            case PlayModeStateChange.EnteredPlayMode:
                OnSelectionChange();
                break;
            case PlayModeStateChange.ExitingPlayMode:
                //OnSelectionChange();
                break; 
        }
    }


    private void OnSelectionChange()
    {
        AIBehaviourTree tree = Selection.activeObject as AIBehaviourTree;
        if(!tree)
        {
            if(Selection.activeGameObject)
            {
                AIBTRunner runner = Selection.activeGameObject.GetComponent<AIBTRunner>();
                if(runner)
                {
                    tree = runner.tree;
                }
            }
        }
        if (Application.isPlaying)
        {
            if (tree != null && treeView!= null)
            {
                treeView.PopulateView(tree);
            }
        }
        else
        {
            if (tree && AssetDatabase.CanOpenAssetInEditor(tree.GetInstanceID()))
            {
                treeView.PopulateView(tree);
            }
        }

        if(tree != null)
        {
            treeObject = new SerializedObject(tree);
            blackboardProperty = treeObject.FindProperty("board");
        }
    }

    void OnNodeSelectionChanged(AINodeView node)
    {
        inspectorView.UpdateSelection(node);
    }

    private void OnInspectorUpdate()
    {
        treeView?.UpdateNodeState();
    }
}