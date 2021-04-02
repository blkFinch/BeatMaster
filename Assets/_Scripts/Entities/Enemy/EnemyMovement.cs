using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    public float moveTime = 1f;
    public bool doRandomMove = false;

    //TODO: Move this to scriptable
    [SerializeField]
    List<int> movePattern;
    private Enemy thisEnemy;
    private Animator animator;
    

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
        Debug.Log("move event triggered");
        if(movePattern.Count > 0){
            //Moves move from start of list to end of list
            int nextMove = movePattern[0];
            movePattern.RemoveAt(0);
            movePattern.Add(nextMove);
            move(nextMove);
        }
        
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

        StartCoroutine(MoveOverDistance(target,moveTime));
        
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
            transform.position = Vector3.Lerp(startPos, target, eleapsedTime/moveDuration);
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
