using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SonicBloom.Koreo;
using SonicBloom.Koreo.Players;


public class PlayMusicButton : MonoBehaviour
{
    private Button button;
    
   void Awake() {
       button = GetComponent<Button>();
       button.onClick.AddListener(PlayMusic);
   }

   private void PlayMusic(){
       Conductor.active.PlayMasterTrack();
   }
}
