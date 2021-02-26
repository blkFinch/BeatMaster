using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo.Players;
using SonicBloom.Koreo;

public class SyncedKoreo : MonoBehaviour
{
    private SimpleMusicPlayer player;
    private bool isPlaying;


    void Awake()
    {
        player = GetComponent<SimpleMusicPlayer>();
    }

    public void PlaySynced(Koreography koreo)
    {
        player.LoadSong(koreo);
        isPlaying = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            player.SeekToSample(MusicManager.active.GetComponent<AudioSource>().timeSamples);
        }

    }
}
