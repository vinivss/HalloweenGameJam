using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] WeaponScriptableObj gunData;
    [SerializeField] 
    float timeSinceLastShot;

    bool CanShoot()
    {
        if(timeSinceLastShot > 1f /(gunData.fireRate /60f))
        {
            return true;
        }

        return false;
    }
    private void Start()
    {
        
    }
    public void Shoot()
    {
        if(CanShoot())
        {
            Debug.Log("Shoot");
        }
    }
}
