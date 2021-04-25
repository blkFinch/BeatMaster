using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public AudioClip heroAtkSound;
    private Vector3 startDashpos; //position of character before animation

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


        if (playerHealthBar != null)
            playerHealthBar.UpdateBar(currentHealth, stats.Hp);
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
        Debug.Log("Damage blocked");

        if (currentHealth < 0){
            Destroy(this.gameObject);
        }
    }

    public void Move(Vector2 movement)
    {
        isoMovement.movementVector = movement;
    }

    public void Block(bool blockValue)
    {
        Debug.Log("is blocking = " + blockValue);
        isBlocking = blockValue;
        //plays block animation if true idle if false
        if (blockValue)
            // spriteRenderer.sprite = blockSprite;
            isoMovement.AnimateBlock(true);
        else
            isoMovement.AnimateBlock(false);
    }

    public void Attack()
    {
        audio.clip = heroAtkSound;
        audio.Play();
        isoMovement.AnimateAttack();
    }

    public void TargetedAttack(GameObject target){
        isoMovement.AnimateTargetedAttack(target, this.transform.position);
    }

    public void EnterCombat()
    {
        playerStateMachine.ChangeState(new PlayerCombatState());
        //stop active movement on combat entery
        Move(new Vector2(0, 0));
    }

    public void ExitCombat()
    {
        playerStateMachine.ChangeState(new PlayerRoamState());
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("COLLISION");
    }

}
