using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRandomPlayClip : MonoBehaviour
{
    public List<AudioClip> audiosClips;

    public List<AudioSource> audioSources;

    private int _index = 0;

    public void PlayRandomAudio(){

        if(_index >= audioSources.Count){
            _index = 0;
        }
        var audioSource = audioSources[_index];

        audioSource.clip = audiosClips[Random.Range(0, audiosClips.Count)];
        audioSource.Play();
        _index++;

    }

}
