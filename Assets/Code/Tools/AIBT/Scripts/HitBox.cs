using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//gets different parts of the hitbox;

public class HitBox : MonoBehaviour
{
    public AIAgent agent;
    Ragdoll ragdoll;

    public void Start()
    {
        agent = GetComponentInParent<AIAgent>();
    }
    public void OnHit(float damage)
    {
        agent.TakeDamage(damage);
    }

    // adding knockback to death.
    public void ApplyForce(Vector3 force)
    {
        var rigidbody = agent.animator.GetBoneTransform(HumanBodyBones.Hips).GetComponent<Rigidbody>();
        rigidbody.AddForce(force, ForceMode.VelocityChange);
    }

    
}
