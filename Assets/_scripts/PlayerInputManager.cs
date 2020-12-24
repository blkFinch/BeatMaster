using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    public delegate void OnAttackButton(AttackType type);
    public static event OnAttackButton onAttack;

    public void OnPlayerBlock(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Hero.active.Block();
        }
    }

    public void PingBlueAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            onAttack(AttackType.BLUE);
        }
    }

    public void PingYellowAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            onAttack(AttackType.YELLOW);
        }
    }

    public void PingRedAttack(InputAction.CallbackContext context)
    {
        Debug.Log("red pressed");
        if (context.performed)
        {
            onAttack(AttackType.RED);
        }
    }

}


//ENUM FOR AttackHandler

public enum AttackType
{
    BLUE,
    YELLOW,
    RED,
    GREEN
}