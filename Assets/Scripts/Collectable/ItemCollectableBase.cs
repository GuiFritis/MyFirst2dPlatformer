using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableBase : MonoBehaviour
{

    public string playerTag = "Player";

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag(playerTag)){
            Collect();
        }
    }

    protected virtual void Collect(){
        gameObject.SetActive(false);
        OnCollect();
    }

    protected virtual void OnCollect(){}
}
