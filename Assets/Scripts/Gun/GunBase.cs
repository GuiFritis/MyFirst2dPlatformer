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

    public AudioRandomPlayClip shootRandomAudio;
    public AudioRandomPlayClip shootHitRandomAudio;

    private Coroutine _shootCoroutine;

    void Awake()
    {
        soIntEnergy.value = 0;
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
        while(soIntEnergy.value > 0){
            Shoot();
            yield return new WaitForSeconds(fireRate);
        }
    }

    public void Shoot(){
        soIntEnergy.value--;
        var projectile = Instantiate(prefabProjectile);
        projectile.side = playerSideReference.lossyScale.x;
        projectile.transform.position = shootPos.position;
        projectile.shotHitRandomAudio = shootHitRandomAudio;
        if(shootRandomAudio != null){
            shootRandomAudio.PlayRandomAudio();
        }

    }
}
