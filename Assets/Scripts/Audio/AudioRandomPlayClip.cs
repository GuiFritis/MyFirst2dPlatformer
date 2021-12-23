using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRandomPlayClip : MonoBehaviour
{
    public List<AudioClip> audiosClips;

    private AudioSource _audioSource;

    public void PlayRandomAudio(){
        if(audiosClips.Count > 0){
            _audioSource.clip = audiosClips[Random.Range(0, audiosClips.Count)];
            _audioSource.Play();
        }
    }

}
