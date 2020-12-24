using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;
public class SnareTarget : MonoBehaviour
{
    public GameObject targetRingPrefab;
    public float canHitTime = 0.5f;
    private bool canBeHit = false;
    private SpriteRenderer sr;
   
   void Start() {
       sr = GetComponent<SpriteRenderer>();
       Koreographer.Instance.RegisterForEvents("snare_hit", onSnareHit);
   }
    
    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "TargetRing"){
            StartCoroutine("CanHitWindow");
        }
    }

    void onSnareHit(KoreographyEvent evt){
        Instantiate(targetRingPrefab, this.transform.position, Quaternion.identity);
    }

    IEnumerator CanHitWindow(){
        canBeHit = true;
        sr.color = Color.green;
        yield return new WaitForSeconds(canHitTime);
        canBeHit = false;
        sr.color = Color.red;
    }
}
