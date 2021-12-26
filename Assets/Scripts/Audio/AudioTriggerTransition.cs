using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioTriggerTransition : MonoBehaviour
{
    public AudioMixerSnapshot snapshot01;
    public AudioMixerSnapshot snapshot02;
    public float transitionTime = 0.5f;

    public string playerTag = "Player";

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag(playerTag)){
            snapshot02.TransitionTo(transitionTime);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.transform.CompareTag(playerTag)){
            snapshot01.TransitionTo(transitionTime);
        }
    }
}
