using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon SO", menuName = "Tools/Weapons/Weapon SO")]
public class WeaponScriptableObj : ScriptableObject
{
    [Tooltip("Name of the Gun.")]
    public new string name;
    [Tooltip("Damage Weapon does.")]
    public float damage;
    [Tooltip("Maximum Range the bullets will travel.")]
    public float maxRange;
    [Header("Cooldowns")]
    [Tooltip("Cooldown Between Shots.")]
    public float fireRate;
    [Tooltip("Amount that the breath takes to shoot.")]
    public float breathTax;

}
