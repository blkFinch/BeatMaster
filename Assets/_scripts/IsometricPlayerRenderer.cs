using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricPlayerRenderer : MonoBehaviour
{
    Animator animator;
    int lastDirection = 3;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    //Sets the animator state to desired direction
    public void SetDirection(Vector2 direction)
    {

        if(direction.magnitude > 0.15){
            lastDirection = DirectionToInt(direction, 4);
            SetMovingDirection(lastDirection);    
        }else{        
            SetIdleDirection(lastDirection);
        }
   
    }

    private void SetMovingDirection(int dir)
    {
        switch (dir)
        {
            case 0:
                animator.Play("Run NW");
                break;

            case 1:
                animator.Play("Run SW");
                break;

            case 2:
                animator.Play("Run SE");
                break;

            case 3:
                animator.Play("Run NE");
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
    private int DirectionToInt(Vector2 move, int sliceCount)
    {
       Vector2 normDir = move.normalized;

        //determine slices in a circle
       float step = 360f / sliceCount;
        float halfStep = step / 2;

        float angle = Vector2.SignedAngle(Vector2.up, normDir);
        angle += halfStep;

        //Make negative angle positive and wrap around
        if(angle < 0){
            angle += 360;
        }

        float stepCount = angle/step;

        return Mathf.FloorToInt(stepCount);

    }

}
