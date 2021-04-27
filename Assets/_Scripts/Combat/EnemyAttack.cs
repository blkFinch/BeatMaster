using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float damageableTime = 0.5f;
    public int damage = 7;
    // Start is called before the first frame update


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

        if(other.gameObject.tag == "Player"){
            Debug.Log("Enemy attacking for " + damage);
            Hero.active.Damage(damage);
            Destroy(this.gameObject);
        }
    }
}
