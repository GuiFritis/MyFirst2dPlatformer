using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public int damage = 1;

    public Animator animator;

    public string triggerAtack = "Attack";

    public HealthBase healthBase;

    void OnCollisionEnter2D(Collision2D collision)
    {
        var healthBase = collision.gameObject.GetComponent<HealthBase>();

        if(healthBase != null){
            healthBase.TakeDamage(damage);
            PlayAttackAnimation();
        }
    }

    private void PlayAttackAnimation(){
        animator.SetTrigger(triggerAtack);
    }

    public void Damage(int amount){
        healthBase.TakeDamage(amount);
    }
}
