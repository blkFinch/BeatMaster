using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this is a temporary way to share song data
public class SongInfo : MonoBehaviour
{
    public static SongInfo active;
    public float bpm = 120f;
    public int beatsPerBar = 4;
    public float secondsPerBeat;

    void Awake() {
        active = this;
    }

    void Start() {
        if(MusicManager.active)
            bpm = MusicManager.active.masterTrack.bpm;
        secondsPerBeat = (this.bpm / this.beatsPerBar) / 60;
    }

}
