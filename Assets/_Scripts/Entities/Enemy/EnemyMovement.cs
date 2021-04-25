using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using SonicBloom.Koreo;

public enum MovementDirections
{
    NORTH, NORTHEAST, EAST, SOUTHEAST, SOUTH, SOUTHWEST, WEST, NORTHWEST
}

public enum EnemyState
{
    AGGRO, IDLE
}

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{

     //todo: move to SO
    [SerializeField]
    AudioClip atkSound;
    public float moveTime = 1f;

    //TODO: Move this to scriptable
    [SerializeField]
    List<MovementDirections> idlePattern;
    private Enemy thisEnemy;
    private Animator animator;
    private AudioSource audioSource;
    private SpriteRenderer sprite;

    [SerializeField]
    private GameObject attackZone;

    private EnemyState state;

    public string moveEventTag;
    public string atkEventTag;
    public EnemyState State { get => state; set => state = value; }

    private MovementDirections currentDirection;

    void Awake()
    {
        

        thisEnemy = GetComponent<Enemy>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        sprite = GetComponent<SpriteRenderer>();
        this.State = EnemyState.IDLE;
        attackZone.GetComponent<EnemyAttack>().damage = thisEnemy.template.Atk;

    }


    // Start is called before the first frame update
    void Start()
    {

        //TODO: remove this once all enemies can animate
        if (thisEnemy.template.canAnimate)
        {
            animator.runtimeAnimatorController = thisEnemy.template.controller;
        }

        Koreographer.Instance.RegisterForEvents(moveEventTag, OnMoveEvent);
        Koreographer.Instance.RegisterForEvents(atkEventTag, OnAttackEvent);
    }

    public void EnterAgro()
    {
        this.State = EnemyState.AGGRO;
    }

    void OnMoveEvent(KoreographyEvent evt)
    {

        switch (State)
        {
            case EnemyState.IDLE:
                IdleMove();
                break;
            case EnemyState.AGGRO:
                AgroMove();
                break;
            default:
                IdleMove();
                break;
        }

    }

    void OnAttackEvent(KoreographyEvent evt)
    {
        switch (State)
        {
            case EnemyState.AGGRO:
                playAttackAnimation(currentDirection);
                if(atkSound){ audioSource.clip = atkSound; audioSource.Play(); }
                break;
            default:
                break;
        }
    }

    private void IdleMove()
    {
        if (idlePattern.Count > 0)
        {
            //Moves move from start of list
            int nextMove = (int)idlePattern[0];
            idlePattern.RemoveAt(0);
            //Returns move to back of list
            MovementDirections dir = (MovementDirections)nextMove;
            idlePattern.Add(dir);
            move(nextMove);
        }
    }

    private void AgroMove()
    {
        Vector3 target = Vector3.MoveTowards(this.transform.position, Hero.active.transform.position, 1f);
        StartCoroutine(MoveOverDistance(target, moveTime));
    }

    public void move(int direction)
    {
        Vector3 target = this.transform.position;
        switch (direction)
        {
            case 0:
                target += Vector3.up;
                break;
            case 1:
                target += Vector3.up + Vector3.right;
                break;
            case 2:
                target += Vector3.right;
                break;
            case 3:
                target -= Vector3.up - Vector3.right;
                break;
            case 4:
                target -= Vector3.up;
                break;
            case 5:
                target -= Vector3.up + Vector3.right;
                break;
            case 6:
                target -= Vector3.right;
                break;
            case 7:
                target += Vector3.up - Vector3.right;
                break;

            default:
                break;


        }

        StartCoroutine(MoveOverDistance(target, moveTime));

    }

    private void setMoveAnimation(Vector2 target)
    {
        float deltaX = transform.position.x - target.x;
        float deltaY = transform.position.y - target.y;

        Debug.Log("delta x / y = " + deltaX + " / " + deltaY);

        if (deltaX < -0.1)
        {   //Moving E
            sprite.flipX = true;
            currentDirection = MovementDirections.EAST;
            animator.Play("MoveE");
        }
        else if (deltaX > 0.1)
        { //Moving W
            sprite.flipX = false;
            currentDirection = MovementDirections.WEST;
            animator.Play("MoveE");
        }
        else
        { //Moving N or S
            if (deltaY > 0)
            {
                currentDirection = MovementDirections.SOUTH;
                animator.Play("MoveS");
            }
            else
            {
                currentDirection = MovementDirections.NORTH;
                animator.Play("MoveN");
            }
        }
        Debug.Log("current dir: " + currentDirection);
    }


    private void playAttackAnimation(MovementDirections dir)
    {
        Debug.Log("Attacking: " + dir);
        switch (dir)
        {
            case MovementDirections.NORTH:
                animator.Play("AtkN");
                Instantiate(attackZone, this.transform.position + transform.up, Quaternion.identity);
                break;
            case MovementDirections.EAST:
                sprite.flipX = false;
                animator.Play("AtkE");
                Instantiate(attackZone, this.transform.position + transform.right, Quaternion.identity);
                break;
            case MovementDirections.WEST:
                sprite.flipX = true;
                animator.Play("AtkE");
                Instantiate(attackZone, this.transform.position - transform.right, Quaternion.identity);
                break;
            default:
                animator.Play("AtkS");
                Instantiate(attackZone, this.transform.position - transform.up, Quaternion.identity);
                break;
        }

    }

    void OnDestroy()
    {
        Koreographer.Instance.UnregisterForAllEvents(this);
    }

    IEnumerator MoveOverDistance(Vector2 target, float moveDuration)
    {
        float eleapsedTime = 0;
        Vector3 startPos = transform.position;
        Debug.Log("target: " + target);
        setMoveAnimation(target);

        while (eleapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(startPos, target, eleapsedTime / moveDuration);
            eleapsedTime += Time.deltaTime;
            yield return null;
        }

        animator.Play("Idle");
    }

    //DEBUGG test
    IEnumerator RandomMove()
    {
        yield return new WaitForSeconds(moveTime);
        int rand = Random.Range(0, 8);
        move(rand);
        // StartCoroutine(RandomMove());
    }
}
