using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Singleton instance of player character 
public class Hero : MonoBehaviour, IDamageable<int>
{
    //COMPONENTS
    private Animator animator;
    private AudioSource audio;
    private IsometricPlayerMovement isoMovement;
    public static Hero active;

    public SimpleHealthBar playerHealthBar;

    //STATS
    public PlayerStats stats;

    private int currentHealth;

    //COMBAT FIELDS
    public PlayerStateMachine playerStateMachine;
    private bool isBlocking;
    public bool IsBlocking { get => isBlocking; }
    public Vector3 startDashpos; //position of character before animation

    //SOUNDS
    public AudioClip heroAtkSound;
    public GameObject syncedBlockLoop;
    private GameObject activeBlockLoop;


    void Awake()
    {
        if (active == null) { active = this; }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerStateMachine = new PlayerStateMachine();
        playerStateMachine.ChangeState(new PlayerDungeonState());
        animator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        isoMovement = GetComponent<IsometricPlayerMovement>();
        //Sets the return position -- this should set OnEnterCombat
        startDashpos = this.gameObject.transform.position;
        currentHealth = stats.Hp;
        Debug.Log("current health is " + currentHealth);

        if (playerHealthBar != null)
            playerHealthBar.UpdateBar(currentHealth, stats.Hp);

        //DELEGATES register
        if (EnemyManager.active)
        {
            EnemyManager.enemyRegisteredDelegate += EnterCombat;
        }
    }

    //TODO: make a listener delegate for Damage functions
    public void Damage(int damage)
    {
        if (!isBlocking)
        {
            currentHealth -= damage;
            if (playerHealthBar != null)
                playerHealthBar.UpdateBar(currentHealth, stats.Hp);
            Debug.Log("Damaged for: " + damage);
        }

        if (currentHealth < 0)
        {
            Destroy(this.gameObject);
            SceneManager.LoadScene("Title");

        }
    }

    public void Heal(int health){
        currentHealth += health;

        if (playerHealthBar != null)
                playerHealthBar.UpdateBar(currentHealth, stats.Hp);
    }

    public void Move(Vector2 movement)
    {
        isoMovement.movementVector = movement;
    }

    public void Block(bool blockValue)
    {
        isBlocking = blockValue;
        //plays block animation if true idle if false
        if (blockValue)
        {
            // spriteRenderer.sprite = blockSprite;
            isoMovement.AnimateBlock(true);
            activeBlockLoop = Instantiate(syncedBlockLoop, transform.position, Quaternion.identity);
        }
        else
        {
            isoMovement.AnimateBlock(false);
            Destroy(activeBlockLoop);
        }

    }

    public void Attack()
    {
        if (!isBlocking)
        {
            audio.clip = heroAtkSound;
            audio.Play();
            isoMovement.AnimateAttack();
        }

    }

    public void TargetedAttack(GameObject target)
    {
        if (!isBlocking)
        {
            audio.clip = heroAtkSound;
            audio.Play();
            isoMovement.AnimateTargetedAttack(target, this.transform.position);
        }
    }

    public void EnterCombat()
    {
        if (this.playerStateMachine.currentState.GetType() != typeof(PlayerCombatState))
        {
            playerStateMachine.ChangeState(new PlayerCombatState());
            startDashpos = this.transform.position;
            //stop active movement on combat entery
            Move(new Vector2(0, 0));
            isoMovement.FreezeConstraints(true);
        }
    }

    public void ExitCombat()
    {
        playerStateMachine.ChangeState(new PlayerRoamState());
        isoMovement.FreezeConstraints(false);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("COLLISION");
    }

    void OnDestroy()
    {
        EnemyManager.enemyRegisteredDelegate -= EnterCombat;
    }

    public bool IsInCombat()
    {
        if (playerStateMachine.currentState.GetType() == typeof(PlayerCombatState))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
