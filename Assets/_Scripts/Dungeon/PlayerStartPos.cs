using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//On start moves player to this position
// Should be used to magnet player to start posistion on new map
public class PlayerStartPos : MonoBehaviour
{
    private Hero player;
    // Start is called before the first frame update
    void Start()
    {
        player = Hero.active;
        if(player){
            player.transform.position = this.transform.position;
        }
    }

    
    void OnDrawGizmos() {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, 1f);
    }

}
