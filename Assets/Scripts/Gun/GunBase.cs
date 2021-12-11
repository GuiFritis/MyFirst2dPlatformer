using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    public ProjectileBase prefabProjectile;

    public Transform shootPos;

    public float fireRate = 0.2f;

    public Transform playerSideReference;

    public SOInt soIntEnergy;

    private Coroutine _shootCoroutine;

    void Start()
    {
        soIntEnergy.value = 10;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S) && soIntEnergy.value > 0){
            _shootCoroutine =  StartCoroutine(nameof(StartShoot));
        } else if(Input.GetKeyUp(KeyCode.S) && _shootCoroutine != null){
            StopCoroutine(_shootCoroutine);
        }
    }

    IEnumerator StartShoot(){
        while(true){
            if(soIntEnergy.value > 0){
                Shoot();
                yield return new WaitForSeconds(fireRate);
            } else {
                StopCoroutine(nameof(_shootCoroutine));
            }
        }
    }

    public void Shoot(){
        var projectile = Instantiate(prefabProjectile);
        projectile.transform.position = shootPos.position;
        projectile.side = playerSideReference.localScale.x;
        soIntEnergy.value--;
    }
}
