using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    private AudioSource source;
    public static SoundEffectManager active;

    void Awake() {
        if(active != null){
            Destroy(this.gameObject);
        }else{
            active = this;
        }

        source = GetComponent<AudioSource>();
    }
   
   public void PlayClip(AudioClip clip){
       source.clip = clip;
       source.Play();
   }
}
