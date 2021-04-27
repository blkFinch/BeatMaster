using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageTrigger : MonoBehaviour
{
    public Enemy parent;


    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "TargetRing")
        {
            Debug.Log("triggered damageable");
            parent.DamageableEvent();
        }
    }
}
