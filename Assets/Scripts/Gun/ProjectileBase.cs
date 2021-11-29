using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{

    public Vector3 direction;

    public float timeToDestroy = 2f;

    public float side = 1f;

    public int damage = 1;

    void Awake()
    {
        Destroy(gameObject, timeToDestroy);
    }

    void Update()
    {
        transform.Translate(direction * Time.deltaTime * side);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var enemy = collision.gameObject.GetComponent<EnemyBase>();

        if(enemy != null){
            enemy.Damage(damage);
            Destroy(gameObject);
        }
    }
}