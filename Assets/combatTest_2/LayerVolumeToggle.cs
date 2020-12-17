using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerVolumeToggle : MonoBehaviour
{
    public int layerToToggle;
    public LayerVolumeController controller;
    private bool isToggled = true;
    
    public void toggleLayer(){
        if(isToggled){
            controller.MuteLayer(layerToToggle);
            isToggled = false;
        }else{
            controller.UnmuteLayer(layerToToggle);
            isToggled = true;
        }
    }
}
