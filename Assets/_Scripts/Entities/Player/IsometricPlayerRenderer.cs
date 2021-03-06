using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricPlayerRenderer : MonoBehaviour
{
    Animator animator;
    int lastDirection = 3;
    public float startRunSpeed = 3f;

    public GameObject attackZone;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void BlockAnimation()
    {
        animator.Play("Block");
    }

    public void UnblockAnimation()
    {
        SetIdleDirection(lastDirection);
    }

    //Sets the animator state to desired direction
    public void SetDirection(Vector2 direction)
    {

        if (direction.magnitude > 0.15)
        {
            lastDirection = DirectionToInt(direction, 4);
            SetMovingDirection(lastDirection, direction.magnitude);
        }
        else
        {
            SetIdleDirection(lastDirection);
        }

    }

    public void SetAttackDirection(Vector2 targetDir)
    {
        int dir = DirectionToInt(targetDir, 4);
        Debug.Log("atk dir : " + dir);
        switch (dir)
        {
            case 0:
                animator.Play("Attack NW");
                // Instantiate(attackZone, this.transform.position - transform.right + transform.up, Quaternion.identity);
                break;

            case 1:
                animator.Play("Attack SW");
                // Instantiate(attackZone, this.transform.position - transform.right - transform.up, Quaternion.identity);
                break;

            case 2:
                animator.Play("Attack SE");
                // Instantiate(attackZone, this.transform.position + transform.right - transform.up, Quaternion.identity);
                break;

            case 3:
                animator.Play("Attack NE");
                // Instantiate(attackZone, this.transform.position + transform.right + transform.up, Quaternion.identity);
                break;
        }
    }

    private void SetMovingDirection(int dir, float moveMagnitude)
    {
        bool isRunning = moveMagnitude > startRunSpeed;

        switch (dir)
        {
            case 0:
                if (isRunning) { animator.Play("Run NW"); }
                else { animator.Play("Walk NW"); }

                break;

            case 1:
                if (isRunning) { animator.Play("Run SW"); }
                else { animator.Play("Walk SW"); }
                break;

            case 2:
                if (isRunning) { animator.Play("Run SE"); }
                else { animator.Play("Walk SE"); }
                break;

            case 3:
                if (isRunning) { animator.Play("Run NE"); }
                else { animator.Play("Walk NE"); }
                break;

            default:
                // animator.Play("Idle SE");
                SetIdleDirection(dir);
                break;
        }
    }

    private void SetIdleDirection(int dir)
    {
        switch (dir)
        {
            case 0:
                animator.Play("Idle NW");
                break;

            case 1:
                animator.Play("Idle SW");
                break;

            case 2:
                animator.Play("Idle SE");
                break;

            case 3:
                animator.Play("Idle NE");
                break;

            default:
                animator.Play("Idle SE");
                break;
        }
    }

    //Taken from  https://www.youtube.com/watch?v=tywt9tOubEY
    //Slices a circle into sections and returns index of section
    public static int DirectionToInt(Vector2 move, int sliceCount)
    {
        Vector2 normDir = move.normalized;

        //determine slices in a circle
        float step = 360f / sliceCount;
        float halfStep = step / 2;

        float angle = Vector2.SignedAngle(Vector2.up, normDir);
        angle += halfStep;

        //Make negative angle positive and wrap around
        if (angle < 0)
        {
            angle += 360;
        }

        float stepCount = angle / step;

        return Mathf.FloorToInt(stepCount);

    }

}
