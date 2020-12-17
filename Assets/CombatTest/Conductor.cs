using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;
using SonicBloom.Koreo.Players;

public class Conductor : MonoBehaviour
{
    public SimpleMusicPlayer masterTrack;
    public static Conductor active = null;
    public Koreography trackToSync;
    public Koreography masterKoreo;
    public GameObject simplePlayerPrefab;

    //TEST generate multi
    public MusicLayer myDeck;
    public MultiMusicPlayer multiMusicPlayer;
    //stats for syncing the audio

    //Current song position, in seconds
    public float songPosition;

    //Current song position, in beats
    public float songPositionInBeats;
    public float beatsPerBar = 4;


    //Que to launch
    private SimpleMusicPlayer armedPlayer = null;

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
        MusicLayer sync = new MusicLayer(trackToSync, null);
        MusicLayer master = new MusicLayer(masterKoreo, null);
        List<MusicLayer> layers = new List<MusicLayer>();
        layers.Add(sync);
        layers.Add(master);

        multiMusicPlayer.LoadSong(layers);
        multiMusicPlayer.Play();
    }

    void Update()
    {
        //launch song if is begininng of bar
        float mod = beatsPerBar % songPositionInBeats;
        if (mod == 4)
        {
            Debug.Log("mod = " + mod);
        }
    }

    public void PlayMasterTrack()
    {

        masterTrack.LoadSong(masterKoreo, 0, false);
        //Register the beat counter to the master track
        string id = masterKoreo.GetTrackAtIndex(0).EventID;
        Koreographer.Instance.RegisterForEvents(id, BeatCounter);
        masterTrack.Play();
    }

    public void PlaySyncedTrack()
    {
        SimpleMusicPlayer newTrack = Instantiate(simplePlayerPrefab).GetComponent<SimpleMusicPlayer>();

        newTrack.LoadSong(trackToSync, 0, false);
        string id = trackToSync.GetTrackAtIndex(0).EventID;
        Koreographer.Instance.RegisterForEvents(id, FireEventDebugLog);
        armedPlayer = newTrack;
    }

    void FireEventDebugLog(KoreographyEvent koreoEvent)
    {
        Debug.Log("Koreography Event Fired.");

    }

    void BeatCounter(KoreographyEvent tic)
    {
        songPositionInBeats++;
        Debug.Log(songPositionInBeats);
    }

}
