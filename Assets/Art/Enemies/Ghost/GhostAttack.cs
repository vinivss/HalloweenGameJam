using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAttack : MonoBehaviour
{
    public MeleeRange AttackRange;
    AIAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        AttackRange = GetComponent<MeleeRange>();
        agent = GetComponent<AIAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
