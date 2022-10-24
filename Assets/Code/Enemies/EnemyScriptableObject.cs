using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy SO", menuName = "Tools/AI/ New Enemy Scriptable Object")]
public class EnemyScriptableObject : ScriptableObject
{
    public enum EnemyType
    {
        Melee,
        Ranged,
        Mixed
    }
    [Header("Type of Enemy")]
    [Tooltip("What type of enemy is this AI? \n Melee: Charges the player and does a melee attack \n Ranged: Runs from the player while attacking from a distance \n Mixed: Changes what attacks it does based on its distance to the player.")]
    [SerializeField] public EnemyType enemyType;
    [Tooltip("is this enemy a boss?")]
    [SerializeField] public bool Boss;


    [Header("Attributes")]
    [Tooltip("Does the AI use smart Pathfinding?")]
    [SerializeField] public bool Smart;
    [Tooltip("How much health will the enemy have?")]
    [SerializeField][Min(0)] public float HealthMax;
    [Header("Movement Attributes")]
    [Tooltip("At what speed does this enemy walk?")]
    [SerializeField][Min(0)] public float WalkSpeed;
    [Tooltip("How fast does this enemy run?")]
    [SerializeField][Min(0)] public float RunSpeed;
    [Header("Attack Attributes")]
    [Tooltip("How fast does this enemy attack?")]
    [SerializeField][Min(0)] public float attackRate;
    [Tooltip("How much damage does this enemy do?")]
    [SerializeField][Min(0)] public int damage;
    [Header("Search Attributes")]
    [Tooltip("How long will this enemy search for someone?")]
    [SerializeField][Min(0)] public int SearchTime;
    [Tooltip("How fast will this enemy turn while searching?")]
    [SerializeField][Min(0)] public int SearchTurnSpeed;


}
