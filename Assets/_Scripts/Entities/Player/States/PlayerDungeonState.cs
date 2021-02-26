using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDungeonState : IPlayerState
{
   public void Enter(){

    }

    public void Exit(){

    }

    public void ProcessInputStick(InputValue value){
         Hero.active.Move(value.Get<Vector2>());
    }

    public void ProcessInputBlue(){
        Hero.active.Attack();
    }

    public void ProcessInputRed(){
        
    }

    public void ProcessInputGreen(){
        
    }

    public void ProcessInputYellow(){
        
    }
}
