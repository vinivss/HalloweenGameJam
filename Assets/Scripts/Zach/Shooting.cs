using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Camera cam;
    public GameObject projectile;
    public Transform barrel;
    public float projectileSpeed = 30;
    private float timeToFire;
    public float fireRate = 4;
    public float reloadtime;
    private Vector3 destination;
    public Rigidbody rb;

    public float replenishRate = 3;
    public float breathmeter = 100;
    public float meterCost;
    public bool reloading = false;
    public bool buffer = false;

    public Animator m_Animator;

    

    // Update is called once per frame
    void Update()
    {
        //if(attacking == false)
        //{
        //    m_Animator.SetBool("Attacking", false);
        //}
        if(reloading == false)
        {
            if (Input.GetMouseButton(0) && Time.time >= timeToFire)
            {
                if (breathmeter > meterCost)
                {

                    timeToFire = Time.time + 1 / fireRate;
                    ShootLazer();
                    m_Animator.SetBool("Attacking", true);
                    StartCoroutine(attackCheck());
                }
            }

            if (Input.GetMouseButton(1))
            {
                StartCoroutine(Reload());
            }

            if (breathmeter < 100)
            {
                if (buffer == false)
                {
                    StartCoroutine(Regen());
                }
            } 
            else
            {
                breathmeter = 100;
            }
        }
        //Debug.Log("breathmeter: " + breathmeter);
    }
    IEnumerator Regen()
    {
        buffer = true;
        yield return new WaitForSeconds(.5f);
        breathmeter += replenishRate;
        buffer = false;
    }
    IEnumerator Reload()
    {
        reloading = true;
        yield return new WaitForSeconds(reloadtime);
        breathmeter = 100;
        reloading = false;
    }
    void ShootLazer()
        {
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            LayerMask mask = LayerMask.GetMask("shootable");
            if (Physics.Raycast(ray, out hit, 100, mask)) { 

                destination = hit.point;
            }
            else
            {
                destination = ray.GetPoint(1000);
            }
            InstantiateProjectile(barrel);
            breathmeter -= meterCost;
    }
    
    void InstantiateProjectile(Transform firePoint)
    {
        var projectileObj = Instantiate(projectile, firePoint.position, Quaternion.LookRotation(destination - firePoint.position)) as GameObject;
        projectileObj.GetComponent<Rigidbody>().velocity = (destination - firePoint.position).normalized * projectileSpeed + rb.velocity;
        //transform.forward * (bulletSpeed + rigidbody.velocity.magnitude * 0.5)
    }

    IEnumerator attackCheck()
    {
        float randomfloat = Random.Range(1.0f, 2.0f);
        int randomint = (int)Mathf.Round(randomfloat);
    
        m_Animator.SetInteger("AnimCycle", randomint);
        yield return new WaitForSeconds(.25f);
        m_Animator.SetBool("Attacking", false);
    }





}
