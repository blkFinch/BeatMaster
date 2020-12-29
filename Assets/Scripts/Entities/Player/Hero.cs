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

    //TODO: extract stats to serializable class
    public int damage = 10;

    [SerializeField]
    private int maxHealth = 50;

    private int currentHealth;

    //COMBAT FIELDS
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
        animator = GetComponent<Animator>();
        isoMovement = GetComponent<IsometricPlayerMovement>();
        //Sets the return position -- this should set OnEnterCombat
        startDashpos = this.gameObject.transform.position;
        currentHealth = maxHealth;
        playerHealthBar.UpdateBar(currentHealth, maxHealth);
    }

    public void Damage(int damage)
    {
        if (!isBlocking)
        {
            currentHealth -= damage;
            playerHealthBar.UpdateBar(currentHealth, maxHealth);
        }
    }

    public void Move(Vector2 movement)
    {
        isoMovement.movementVector = movement;
    }

    public void Block(bool blockValue)
    {
        isBlocking = blockValue;
    }

    public void Attack(GameObject target)
    {
        //TODO: logic for dash direction
        animator.SetTrigger("dash_up_right");

        // Uncomment this to have hero reset where she starts her dash from
        startDashpos = transform.position;

        //set the target position right in front of target
        Vector3 targetPos = target.gameObject.transform.position - target.gameObject.transform.up;
        StartCoroutine(DashMove(targetPos, startDashpos));
    }

    //TODO: refactor this move to fit with beat
    //Dashes to target, waits 0.5 secs, dash back
    //0.5 is one beat for 120 bpm
    private IEnumerator DashMove(Vector3 end, Vector3 start)
    {
        transform.position = end;
        yield return new WaitForSeconds(0.5f);
        transform.position = start;
    }

}
