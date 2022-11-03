using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools.Trees.AI;
public class AIBTRunner : MonoBehaviour
{
    public AIBehaviourTree tree;
    [SerializeField] GameObject particle;

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

        if(tree.rootNode.state == AINode.State.SUCC)
        {
            Instantiate(particle);
            particle.transform.position = this.transform.position;
            Destroy(gameObject);
        }
    }
}
