using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;

public class Enemy : MonoBehaviour
{
    public CTInstrument instrument;
    public SimpleHealthBar snareHealthBar;
    public int healthMax = 50;
    public int healthCurrent;
    public KeyCode atkKeyCode;
    public GameObject targetRingPrefab;
    public string damageableEventTag;
    public float damageableTime = 0.25f;
    private bool isDamageable = false;
    private SpriteRenderer sr;
    // Start is called before the first frame update
  void Start() {
       sr = GetComponent<SpriteRenderer>();
       Koreographer.Instance.RegisterForEvents(damageableEventTag, onDamageableEvent);
       healthCurrent = healthMax;
       snareHealthBar.UpdateBar(healthCurrent,healthMax);
   }

   void Update() {
       if(isDamageable && Input.GetKeyDown(atkKeyCode)){
          damage(10);
       }
   }

   public void damage(int damage){
       healthCurrent -= damage;
       snareHealthBar.UpdateBar(healthCurrent,healthMax);
       if(healthCurrent <= 0){
           Conductor.active.muteTrack(instrument);
           //Unregester for all events before destroying object
           Koreographer.Instance.UnregisterForEvents(damageableEventTag, onDamageableEvent);
           Destroy(this.gameObject);
       }
   }
    
    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "TargetRing"){
            StartCoroutine("CanHitWindow");
        }
    }

    void onDamageableEvent(KoreographyEvent evt){
        Instantiate(targetRingPrefab, this.transform.position, Quaternion.identity);
    }

    IEnumerator CanHitWindow(){
        isDamageable = true;
        sr.color = Color.green;
        yield return new WaitForSeconds(damageableTime);
        isDamageable = false;
        sr.color = Color.red;
    }
}
