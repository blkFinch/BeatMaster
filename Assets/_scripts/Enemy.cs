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
    public AttackType attackType;
    public string atkButtonName;
    public GameObject targetRingPrefab;
    public GameObject atkPrefab;
    public string damageableEventTag;
    public string attackEventTag;
    public float damageableTime = 0.25f;
    private bool isDamageable = false;
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        Koreographer.Instance.RegisterForEvents(damageableEventTag, onDamageableEvent);
        Koreographer.Instance.RegisterForEvents(attackEventTag, onInstrumentAttackEvent);
        healthCurrent = healthMax;
        snareHealthBar.UpdateBar(healthCurrent, healthMax);
        PlayerInputManager.onAttack += OnPlayerAttack;
    }


    public void OnPlayerAttack(AttackType type)
    {
        if (isDamageable && (type == attackType))
        {
            Hero.active.Attack(this.gameObject);
            damage(Hero.active.damage);
        }

    }

    public void damage(int damage)
    {
        healthCurrent -= damage;
        snareHealthBar.UpdateBar(healthCurrent, healthMax);
        if (healthCurrent <= 0)
        {
            Conductor.active.muteTrack(instrument);
            //Unregester for all events before destroying object
            Koreographer.Instance.UnregisterForEvents(damageableEventTag, onDamageableEvent);
            Koreographer.Instance.UnregisterForEvents(attackEventTag, onInstrumentAttackEvent);
            PlayerInputManager.onAttack -= OnPlayerAttack;
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "TargetRing")
        {
            StartCoroutine("CanHitWindow");
        }
    }

    void onInstrumentAttackEvent(KoreographyEvent evt){
       GameObject atk = Instantiate(atkPrefab, transform.position, Quaternion.identity);
       atk.GetComponent<Target>().target = Hero.active.gameObject.transform;
    }

    void onDamageableEvent(KoreographyEvent evt)
    {
        Instantiate(targetRingPrefab, this.transform.position, Quaternion.identity);
    }

    //Accepts player input to damage for specified damageableTime
    IEnumerator CanHitWindow()
    {
        isDamageable = true;
        sr.color = Color.green;
        yield return new WaitForSeconds(damageableTime);
        isDamageable = false;
        sr.color = Color.red;
    }
}
