using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools.Trees.AI;

public class lazer : MonoBehaviour
{


    public int damage = 15;
    public float lifetime;
    public GameObject particle;

    AIAgent hitPlayer;

    void Start()
    {
        StartCoroutine(wait());
    }

    private bool canhit = false;
    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter(Collider other)
    {

        //Debug.LogError("Colliding");
        if (other.transform.tag == "Enemy")
        {
            hitPlayer = other.GetComponent<AIAgent>();
            hitPlayer.currentHealth -= damage;
            Debug.Log("enemyHealth:" + hitPlayer.currentHealth);
            Destroy(gameObject);

        } 
        if(canhit == true)
        {
            Instantiate(particle, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(.1f);
        canhit = true;
    }

}
