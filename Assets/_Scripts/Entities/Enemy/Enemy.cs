using System.Collections;
using UnityEngine;
using SonicBloom.Koreo;

public class Enemy : MonoBehaviour, IDamageable<int>, IKillable
{
    public CTInstrument instrument;
    public SimpleHealthBar activeHealthBar;
    public GameObject healthBar;
    private GameObject hb;
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

        hb = Instantiate(healthBar, transform.position, Quaternion.identity);
        Canvas canvas = FindObjectOfType<Canvas>();
        hb.transform.SetParent(canvas.transform);

        activeHealthBar = hb.GetComponentInChildren<SimpleHealthBar>();

        hb.GetComponent<HealthbarFollow>().target = this.transform;

        if (activeHealthBar != null)
            activeHealthBar.UpdateBar(healthCurrent, healthMax);

        Conductor.active.UnmuteTrack(instrument);
    }

    void Update()
    {
        float dist = (Hero.active.transform.position - transform.position).sqrMagnitude;
    }


    public void OnPlayerAttack()
    {
        if (isDamageable)
        {
            Hero.active.Attack(this.gameObject);
            Damage(Hero.active.damage);
        }

    }

    public void Damage(int damage)
    {
        healthCurrent -= damage;
        activeHealthBar.UpdateBar(healthCurrent, healthMax);
        if (healthCurrent <= 0)
        {
            EnemyFormation.active.DestructEnemy(this);
        }
    }

    public void Kill()
    {
        Conductor.active.muteTrack(instrument);
        Destroy(hb);
        //Unregester for all events before destroying object
        Koreographer.Instance.UnregisterForEvents(damageableEventTag, onDamageableEvent);
        Koreographer.Instance.UnregisterForEvents(attackEventTag, onInstrumentAttackEvent);
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "TargetRing")
        {
            StartCoroutine("CanHitWindow");
        }
    }

    void onInstrumentAttackEvent(KoreographyEvent evt)
    {
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
