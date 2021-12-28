using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public string playerTag = "Player";

    public GameObject uiEndGame;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag(playerTag)){
            CallEndGame();
        }
    }

    private void CallEndGame(){
        uiEndGame.SetActive(true);
    }
}
