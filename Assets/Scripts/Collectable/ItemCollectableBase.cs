using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableBase : MonoBehaviour
{

    public string playerTag = "Player";
    public ParticleSystem collectParticleSystem;
    public float hideDelay = 1f;
    public SpriteRenderer collectableSprite;
    public Collider2D collider2d;
    
    [Header("Sounds")]
    public AudioSource audioSorce;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag(playerTag)){
            Collect();
            if(collider2d != null){
                collider2d.enabled = false;
            }
        }
    }

    protected virtual void Collect(){
        Invoke(nameof(HideObject), hideDelay);
        OnCollect();
        if(audioSorce != null){
            audioSorce.Play();
        }
    }

    protected void HideObject(){        
        gameObject.SetActive(false);
    }

    protected virtual void OnCollect(){
        if(collectParticleSystem != null){
            collectParticleSystem.Play();
        }
    }
}
