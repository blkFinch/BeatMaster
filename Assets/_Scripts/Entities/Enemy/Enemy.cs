using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using SonicBloom.Koreo;
using TMPro;


public class Enemy : MonoBehaviour, IDamageable<float>, IKillable
{
    [SerializeField]
    private float damageableDelay = 1f;
    public float currentHealth;
    public float atk;
    public EnemyObject template;
    public AttackType type;
    public GameObject targetRingPrefab;
    public string damageableTag;

    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private EnemyMovement mvt;
    private bool isDamageable = false;

    //Delegate for notifying enemy damaged
    public delegate void OnEnemyDamaged(float dam);
    public static OnEnemyDamaged enemyDamagedDelegate;

    private TextMeshPro text;

    void Awake()
    {
        Assert.IsNotNull(template);

        spriteRenderer = this.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = template.sprite;
        
        mvt = this.GetComponent<EnemyMovement>();
        // atk = template.Atk;
        // hpDisplay.UpdateHpDisplay(currentHealth);
        text = GetComponentInChildren<TextMeshPro>();
        Debug.Log("awake called");
    }

    void Start()
    {
        Koreographer.Instance.RegisterForEvents(damageableTag, onDamageableEvent);
        EnemyManager.enemyRegisteredDelegate += onEnemyRegisteredEvent;

        currentHealth = template.maxHealth;
        text.text = currentHealth.ToString();
    }

    public void Damage(float damageTaken)
    {
        Debug.Log("Enemy DAMAGED");
        //Only fire delegate if there are registered listeners
        if (enemyDamagedDelegate != null)
            enemyDamagedDelegate(damageTaken);

        if (isDamageable)
            currentHealth -= damageTaken;
        else 
            Debug.LogError("ENEMY NOT DAMAGEABLE");
        if (currentHealth <= 0)
        {
            Debug.Log("current health: " + currentHealth + " killing obj");
            Kill();
        }else{
            text.text = currentHealth.ToString();
        }
    }

    void onDamageableEvent(KoreographyEvent evt)
    {
        if (mvt.State == EnemyState.COMBAT){

            Instantiate(targetRingPrefab, this.transform.position, Quaternion.identity);
            StartCoroutine("CanHitWindow");
        }
    }

    void onEnemyRegisteredEvent(){
        if(this.mvt.State != EnemyState.COMBAT){
            this.mvt.EnterAgro();
        }
    }

    //Remember to unregister all listeners here
    public void Kill()
    {
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {

            if (mvt.State != EnemyState.COMBAT)
            {
                mvt.EnterCombat();
                EnemyManager.active.RegisterCombatEnemy(this);
            }
        }
    }

    public void DamageableEvent()
    {
        Debug.Log("damagable event");
        StartCoroutine("CanHitWindow");
    }

    IEnumerator CanHitWindow()
    {
        //Wait for ring to shrink. Defaults to one beat before become damageable
        yield return new WaitForSeconds(SongInfo.active.secondsPerBeat * damageableDelay);

        isDamageable = true;
        Debug.Log("ENEMY IS DAM");
        spriteRenderer.color = Color.green;
        yield return new WaitForSeconds(SongInfo.active.secondsPerBeat);
        isDamageable = false;
        Debug.Log("ENEMY NOT DAM");
        spriteRenderer.color = Color.white;
    }

    void OnDestroy() {
        EnemyManager.active.DeregisterEnemy(this);
        Koreographer.Instance.UnregisterForAllEvents(this);
    }

}

/*
    Old enemy style 
    todo: remove 
*/
namespace Deprecated
{
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
                Hero.active.TargetedAttack(this.gameObject);
                Damage(Hero.active.stats.Atk);
            }

        }

        public void Damage(int damage)
        {
            healthCurrent -= damage;
            activeHealthBar.UpdateBar(healthCurrent, healthMax);
            if (healthCurrent <= 0)
            {
                // EnemyFormation.active.DestructEnemy(this);
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

}

