using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lazer : MonoBehaviour
{
    public int damage = 15;
    public float lifetime;
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
        Debug.LogError("Colliding");
        if (other.transform.tag == "Enemy")
        {

            //other.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            //Destroy(gameObject);

        } 
        if(canhit == true)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(.01f);
        canhit = true;
    }

}
