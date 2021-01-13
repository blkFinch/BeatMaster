using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    
    public void OnBlock(InputValue value)
    {
        Hero.active.Block(value.isPressed);
    }
    
    public void OnGreen()
    {
        Hero.active.playerStateMachine.InputGreen();
    }

    
    public void OnYellow()
    {
        Hero.active.playerStateMachine.InputYellow();
    }

    public void OnRed()
    {
        Hero.active.playerStateMachine.InputRed();
    }

    public void OnBlue(){
        Hero.active.playerStateMachine.InputBlue();
    }

    //Callback from Player Move Input
    public void OnMove(InputValue value){
        Hero.active.playerStateMachine.InputStick(value);
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