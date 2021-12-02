using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public int damage = 1;

    public Animator animator;

    public string triggerAtack = "Attack";
    public string triggetDie = "Die";

    public HealthBase healthBase;

    void Awake()
    {
        if(healthBase != null){
            healthBase.OnDeath += OnEnemyDeath;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var healthBase = collision.gameObject.GetComponent<HealthBase>();

        if(healthBase != null){
            healthBase.TakeDamage(damage);
            this.healthBase.TakeDamage(1);
            PlayAttackAnimation();
        }
    }

    private void PlayAttackAnimation(){
        animator.SetTrigger(triggerAtack);
    }

    private void PlayDieAnimation(){
        animator.SetTrigger(triggetDie);
    }

    public void Damage(int amount){
        healthBase.TakeDamage(amount);
    }

    private void OnEnemyDeath(){
        healthBase.OnDeath -= OnEnemyDeath;
        PlayDieAnimation();        
    }
}
