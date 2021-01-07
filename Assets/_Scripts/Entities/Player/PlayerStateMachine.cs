using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IPlayerState
{
    void Enter();
    void Exit();
    void ProcessInputRed();
    void ProcessInputBlue();
    void ProcessInputGreen();
    void ProcessInputYellow();
    void ProcessInputStick(InputValue value);
}

public class PlayerStateMachine : MonoBehaviour
{
    IPlayerState currentState;
    
     public void ChangeState(IPlayerState newState)
    {
        if (currentState != null)
            currentState.Exit();

        currentState = newState;
        currentState.Enter();
    }

    public void InputRed(){
        currentState.ProcessInputRed();
    }

    public void InputBlue(){
        currentState.ProcessInputBlue();
    }

    public void InputGreen(){
        currentState.ProcessInputGreen();
    }

    public void InputYellow(){
        currentState.ProcessInputYellow();
    }

    public void InputStick(InputValue value){
        currentState.ProcessInputStick(value);
    }
}
