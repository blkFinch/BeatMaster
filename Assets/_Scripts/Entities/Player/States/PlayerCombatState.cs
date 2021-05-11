using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombatState : IPlayerState
{
    //Assign targets to the face buttons
    private Enemy redTarget;
    private Enemy blueTarget;
    private Enemy yellowTarget;

    public void Enter()
    {

        //todo: rather than check target on every button press, simply add new target every time a new enemy enters combat
        // if (EnemyManager.active.GetActiveEnemy())
        // {
        //     AssignTarget(EnemyManager.active.GetActiveEnemy());
        // }
        // else
        // {
        //     Debug.LogError("ERR: No active enemy on combat enter");
        //     Hero.active.ExitCombat();
        // }
    }


    public void Exit()
    {

    }

    public void ProcessInputRed()
    {
        Debug.Log("RED");
        if ( redTarget = EnemyManager.active.GetRedEnemy())
            Hero.active.TargetedAttack(redTarget.gameObject);
        // redTarget.OnPlayerAttack();
    }

    public void ProcessInputGreen()
    {
        //Process Input Button Pressed
        //DODGE?
    }

    public void ProcessInputBlue()
    {
        if (blueTarget = EnemyManager.active.GetBlueEnemy())
            Hero.active.TargetedAttack(blueTarget.gameObject);
        // blueTarget.OnPlayerAttack();
    }

    public void ProcessInputYellow()
    {
        if(yellowTarget = EnemyManager.active.GetYellowEnemy())
            Hero.active.TargetedAttack(yellowTarget.gameObject);
        // yellowTarget.OnPlayerAttack();
    }

    public void ProcessInputStick(InputValue value)
    {

    }

    //Assigns enemy to be targeted by attack button
    private void AssignTarget(Enemy enemy)
    {
        switch (enemy.type)
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
                Debug.Log("ERR INCOMPATIBLE ENEMY TYPE " + enemy.type);
                break;
        }
    }
}
