using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinBullet : MonoBehaviour
{
    public int damage = 15;
    public float lifetime;

    PlayerManager hitPlayer;

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            hitPlayer = other.GetComponent<PlayerManager>();
            hitPlayer.health -= damage;
            Destroy(gameObject);
        }

        Destroy(gameObject);
    }
}
