﻿using System.Collections;
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
            //Checks if context is an "OnPress" or "OnRelease" by
            //reading the value as a float:
            //  1 = OnPress
            // anything else is a release
            if(context.ReadValue<float>() < 1){
                Hero.active.Block(false);
            }else{
                Hero.active.Block(true);
            }
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

    public void Move(InputAction.CallbackContext context){
        Vector2 inputVector = context.ReadValue<Vector2>();
        Hero.active.Move(inputVector);
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