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

    public void AnimateAttack(GameObject target, Vector3 startDashpos)
    {
        animatorBusy = true;
         // Uncomment this to have hero reset where she starts her dash from
        startDashpos = transform.position;

        //set the target position right in front of target
        Vector3 targetPos = target.gameObject.transform.position - target.gameObject.transform.up;
        isometricPlayerRenderer.SetAttackDirection(targetPos);
        StartCoroutine(DashMove(targetPos, startDashpos));
    }

    //TODO: refactor this move to fit with beat
    //Dashes to target, waits 0.5 secs, dash back
    //0.5 is one beat for 120 bpm
    private IEnumerator DashMove(Vector3 end, Vector3 start)
    {
        transform.position = end;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(0.5f);
        transform.position = start;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        animatorBusy = false;
    }
}
