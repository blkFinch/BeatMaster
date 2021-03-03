using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float damageableTime = 0.5f;
    public float damage = 5f;
    // Start is called before the first frame update

    void Awake() {
        //todo: pull this out??
        damage = Hero.active.stats.Atk;
        Debug.Log("damage " + damage);
    }

    void Start()
    {
        StartCoroutine(attackRoutine());
    }

    //Keeps attack alive for desired time. Should be one beat
    IEnumerator attackRoutine(){
        yield return new WaitForSeconds(damageableTime);
        Destroy(this.gameObject);
    }

    void OnTriggerStay2D(Collider2D other) {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        
        //todo: test this on non enemy collider
        if(enemy != null){
            enemy.Damage(this.damage);
            //Attack can only damage once so destroy at this point
            Destroy(this.gameObject);
        }
    }
}
