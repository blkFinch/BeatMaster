using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Conductor : MonoBehaviour
{
    public static Conductor active = null;

    public LayerVolumeController volumeControl;

    //SONG INFO
    public float bpm = 120f;

    //for now this will leave out the pad as that should stay playing for the
    //whole dungeon
    public CTInstrument[] stems;

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

    void Start()
    {
        foreach (CTInstrument instrument in stems)
        {
            Debug.Log("muting" + instrument);
            muteTrack(instrument);
        }
    }

    public void muteTrack(CTInstrument instrument)
    {
        volumeControl.MuteLayer((int)instrument);
    }

    public void UnmuteTrack(CTInstrument instrument){
        volumeControl.UnmuteLayer((int)instrument);
    }
}
