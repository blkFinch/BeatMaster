using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    
    public void OnBlock(InputAction.CallbackContext context)
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