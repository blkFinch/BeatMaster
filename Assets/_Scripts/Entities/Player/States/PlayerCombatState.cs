using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombatState : IPlayerState
{
    //Assign targets to the face buttons
    private Deprecated.Enemy redTarget;
    private Deprecated.Enemy blueTarget;
    private Deprecated.Enemy yellowTarget;

    public void Enter()
    {

        if (EnemyFormation.active)
        {
            //Assign targets from formation
            foreach (Deprecated.Enemy enemy in EnemyFormation.active.enemies)
            {
                AssignTarget(enemy);
            }
        }
        else
        {
            Debug.Log("ERR: No enemy formation on combat enter");
            Hero.active.ExitCombat();
        }
    }


    public void Exit()
    {

    }

    public void ProcessInputRed()
    {
        if(redTarget)
            redTarget.OnPlayerAttack();
    }

    public void ProcessInputGreen()
    {
        //Process Input Button Pressed
        //DODGE?
    }

    public void ProcessInputBlue()
    {
       if(blueTarget)
            blueTarget.OnPlayerAttack();
    }

    public void ProcessInputYellow()
    {
        if(yellowTarget)
            yellowTarget.OnPlayerAttack();
    }

    public void ProcessInputStick(InputValue value)
    {

    }

    //Assigns enemy to be targeted by attack button
    private void AssignTarget(Deprecated.Enemy enemy)
    {
        switch (enemy.attackType)
        {
            case AttackType.RED:
                redTarget = enemy;
                break;
            case AttackType.BLUE:
                blueTarget = enemy;
                break;
            case AttackType.YELLOW:
                yellowTarget = enemy;
                break;
            default:
                Debug.Log("ERR INCOMPATIBLE ENEMY TYPE " + enemy.attackType);
                break;
        }
    }
}
