 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using SonicBloom.Koreo.Players;

public class MusicManager : MonoBehaviour
{
    public static MusicManager active;
    public SampleObject masterTrack;
    public SimpleMusicPlayer simpleMusicPlayer;

    void Awake() {
        Assert.IsNotNull(masterTrack);

        if(active == null){
            active = this;
        }else{
            Destroy(this.gameObject);
        }

        simpleMusicPlayer = GetComponent<SimpleMusicPlayer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //loads song from masterTrack object
        simpleMusicPlayer.LoadSong(masterTrack.koreography,0, true);
    }

}
