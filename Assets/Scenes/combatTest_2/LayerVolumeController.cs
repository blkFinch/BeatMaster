using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo.Players;
// Put this directly on a MultiMusicPlayer to use
public class LayerVolumeController : MonoBehaviour
{
    public MultiMusicPlayer player;
    

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<MultiMusicPlayer>();
    }

    public void MuteLayer(int layerIndex)
    {
        player.SetVolumeForLayer(layerIndex, 0f);
    }

    public void UnmuteLayer(int layerIndex)
    {
        player.SetVolumeForLayer(layerIndex, 1f);
        
    }

}
