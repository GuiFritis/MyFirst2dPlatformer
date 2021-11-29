using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    public ProjectileBase prefabProjectile;

    public Transform shootPos;

    public float fireRate = 0.2f;

    public Transform playerSideReference;

    private Coroutine _shootCoroutine;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S)){
            _shootCoroutine =  StartCoroutine(nameof(StartShoot));
        } else if(Input.GetKeyUp(KeyCode.S) && _shootCoroutine != null){
            StopCoroutine(_shootCoroutine);
        }
    }

    IEnumerator StartShoot(){
        while(true){
            Shoot();
            yield return new WaitForSeconds(fireRate);
        }
    }

    public void Shoot(){
        var projectile = Instantiate(prefabProjectile);
        projectile.transform.position = shootPos.position;
        projectile.side = playerSideReference.localScale.x;
    }
}
