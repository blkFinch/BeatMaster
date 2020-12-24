using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;
using SonicBloom.Koreo.Players;

public class Conductor : MonoBehaviour
{
    public static Conductor active = null;

    public LayerVolumeController volumeControl;
    //stats for syncing the audio


    void Awake()
    {
        if (active == null)
        {
            active = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void muteTrack(CTInstrument instrument){
        volumeControl.MuteLayer((int)instrument);
    }
    
    void Update()
    {
        //launch song if is begininng of bar
        // float mod = beatsPerBar % songPositionInBeats;
        // if (mod == 4)
        // {
        //     Debug.Log("mod = " + mod);
        // }
    }


}
