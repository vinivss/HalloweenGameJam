using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools.Trees.AI;
public class AIBTRunner : MonoBehaviour
{
    public AIBehaviourTree tree;

    // Start is called before the first frame update
    void Start()
    {
        tree = tree.Clone();
        tree.Bind(GetComponent<AIAgent>());
    }

    // Update is called once per frame
    void Update()
    {
        tree.Update();
    }
}
