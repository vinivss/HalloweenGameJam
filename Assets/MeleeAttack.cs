using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField]AIAgent agent;
    [SerializeField]MeleeRange range;
    GameObject Player;
    bool canAttack = true;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<AIAgent>();
        range = GetComponent<MeleeRange>();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.MsensorRange.Objects.Count > 0 && canAttack)
        {
            Player.GetComponent<PlayerManager>().health -= agent.damage;
            canAttack = false;
            StartCoroutine(WaitforAttackRate());
        }
    }

    IEnumerator WaitforAttackRate()
    {
        yield return new WaitForSeconds(agent.attackRate);
        canAttack = true;
    }
}
 