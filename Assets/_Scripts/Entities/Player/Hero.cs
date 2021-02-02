using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Singleton instance of player character 
public class Hero : MonoBehaviour, IDamageable<int>
{
    //COMPONENTS
    private Animator animator;
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
        playerStateMachine.ChangeState(new PlayerRoamState());
        animator = GetComponent<Animator>();
        isoMovement = GetComponent<IsometricPlayerMovement>();
        //Sets the return position -- this should set OnEnterCombat
        startDashpos = this.gameObject.transform.position;
        currentHealth = stats.Hp;


        if (playerHealthBar != null)
            playerHealthBar.UpdateBar(currentHealth, stats.Hp);
    }

    public void Damage(int damage)
    {
        if (!isBlocking)
        {
            currentHealth -= damage;
            playerHealthBar.UpdateBar(currentHealth, stats.Hp);
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
            isoMovement.isometricPlayerRenderer.BlockAnimation();
        else
            isoMovement.isometricPlayerRenderer.UnblockAnimation();
    }

    public void Attack(GameObject target)
    {
        isoMovement.AnimateAttack(target, startDashpos);
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
