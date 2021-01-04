using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ExitRoomTrigger : MonoBehaviour
{
    [SerializeField]
    private int roomToLoad;
    void Start() {
        Assert.IsNotNull<SceneManager>(SceneManager.active);
    }

   
    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            SceneManager.active.LoadRoom(roomToLoad);
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(this.transform.position, new Vector3(1,1,1));
    }

     void OnValidate() {
        Assert.IsNotNull<SceneManager>(SceneManager.active);
    }

}
