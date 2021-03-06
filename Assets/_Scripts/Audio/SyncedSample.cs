using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncedSample : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip clip;

    void Awake() {
        audioSource = GetComponent<AudioSource>();
    }
    void Start() {
        PlaySynced();
    }
    
    public void PlaySynced(){
        audioSource.clip = this.clip;
        audioSource.Play();
    }

    void Update() {
        if(audioSource.isPlaying){
            audioSource.timeSamples = MusicManager.active.GetComponent<AudioSource>().timeSamples;
        }
    }
}
