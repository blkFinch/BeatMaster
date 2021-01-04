using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public static SceneManager active;
    public GameObject[] rooms;
    public GameObject activeRoom;

    void Awake() {
        if(active == null){active = this; }
        else{
            Destroy(this.gameObject);
        }
    }
    
    public void LoadRoom(int index){
        Destroy(activeRoom);
        activeRoom = Instantiate(rooms[index], this.transform.position, Quaternion.identity);
    }
}
