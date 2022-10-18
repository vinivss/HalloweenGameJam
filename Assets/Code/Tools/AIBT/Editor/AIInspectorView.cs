using System;
using System.Collections;
using System.Collections.Generic;
using Tools.Trees.AI;
using UnityEngine.UIElements;
using UnityEditor;

public class AIInspectorView : VisualElement
{
    public new class UxmlFactory : UxmlFactory<AIInspectorView, VisualElement.UxmlTraits> { };
    Editor editor;
    public AIInspectorView()
    {

    }

    internal void UpdateSelection(AINodeView node)
    {
        Clear();

        UnityEngine.Object.DestroyImmediate(editor);
        editor = Editor.CreateEditor(node.node);
        IMGUIContainer container = new IMGUIContainer(() =>
        {
            if (editor.target)
            {
                editor.OnInspectorGUI();
            }
        });
        Add(container);
    }
}
