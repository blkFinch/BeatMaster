using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricPlayerMovement : MonoBehaviour
{
    public float moveSpeed = 1f;
    Rigidbody2D rb;
    public IsometricPlayerRenderer isometricPlayerRenderer;
    private bool animatorBusy = false;

    public Vector2 movementVector;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        isometricPlayerRenderer = GetComponent<IsometricPlayerRenderer>();
        movementVector = new Vector2(0, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!animatorBusy)
        {
            Vector2 currentPos = rb.position;
            movementVector = Vector2.ClampMagnitude(movementVector, 1);
            Vector2 movement = movementVector * moveSpeed;
            Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
            isometricPlayerRenderer.SetDirection(movement);
            rb.MovePosition(newPos);
        }
    }

    //Animates a dash attack to a desired target
    public void AnimateTargetedAttack(GameObject target, Vector3 startDashpos)
    {
        animatorBusy = true;
         // Uncomment this to have hero reset where she starts her dash from
        startDashpos = Hero.active.startDashpos;

        //set the target position right in front of target
        Vector3 targetPos = target.gameObject.transform.position - target.gameObject.transform.up;
        isometricPlayerRenderer.SetAttackDirection(targetPos);
        StartCoroutine(DashMove(targetPos, startDashpos, target.GetComponent<Enemy>()));
    }

    //Animate untargeted attack
    public void AnimateAttack(){
        animatorBusy = true;
        isometricPlayerRenderer.SetAttackDirection(movementVector);
        StartCoroutine(AttackAnimationPause());
    }

    public void AnimateBlock(bool isBlocking){
        if(isBlocking){
            isometricPlayerRenderer.BlockAnimation();
            animatorBusy = true;
        }else{
            isometricPlayerRenderer.UnblockAnimation();
            animatorBusy = false;
        }
    }

    private IEnumerator AttackAnimationPause(){
        yield return new WaitForSeconds(SongInfo.active.secondsPerBeat);
        animatorBusy = false;
    }

    private IEnumerator DashMove(Vector3 end, Vector3 start, Enemy enemy = null)
    {
        transform.position = end;
        // rb.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(SongInfo.active.secondsPerBeat);
        transform.position = start;
        // rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        // if(enemy){ enemy.Damage(Hero.active.stats.Atk); }
        animatorBusy = false;
    }

    public void FreezeConstraints(bool doFreeze){
        if(doFreeze){
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }   
        else{
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }   
    }
}
