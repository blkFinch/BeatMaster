using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;

public enum MovementDirections
{
    NORTH, NORTHEAST, EAST, SOUTHEAST, SOUTH, SOUTHWEST, WEST, NORTHWEST
}

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    public float moveTime = 1f;

    //TODO: remove random move from move evnt
    public bool doRandomMove = false;

    //TODO: Move this to scriptable
    [SerializeField]
    List<MovementDirections> idlePattern;
    private Enemy thisEnemy;
    private Animator animator;

    //TODO: refactor to state machine?
    private bool isIdle = true;
    public bool getIsIdle()
    {
        return isIdle;
    }
    public void setIdle(bool idleState)
    {
        isIdle = idleState;
    }


    public string moveEventTag;

    void Awake()
    {
        thisEnemy = GetComponent<Enemy>();
        animator = GetComponent<Animator>();
    }


    // Start is called before the first frame update
    void Start()
    {
        if (doRandomMove)
        {
            StartCoroutine(RandomMove());
        }

        //TODO: remove this once all enemies can animate
        if (thisEnemy.template.canAnimate)
        {
            animator.runtimeAnimatorController = thisEnemy.template.controller;
        }

        Koreographer.Instance.RegisterForEvents(moveEventTag, OnMoveEvent);
    }

    void OnMoveEvent(KoreographyEvent evt)
    {

        if (isIdle)
        {
            IdleMove();
        }
        else
        {
            AgroMove();
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
                target -= Vector3.up + Vector3.right;
                break;
            case 4:
                target -= Vector3.up;
                break;
            case 5:
                target -= Vector3.up - Vector3.right;
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

    void OnDestroy()
    {
        Koreographer.Instance.UnregisterForEvents(moveEventTag, OnMoveEvent);
    }

    IEnumerator MoveOverDistance(Vector2 target, float moveDuration)
    {
        float eleapsedTime = 0;
        Vector3 startPos = transform.position;

        while (eleapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(startPos, target, eleapsedTime / moveDuration);
            eleapsedTime += Time.deltaTime;
            yield return null;
        }
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
