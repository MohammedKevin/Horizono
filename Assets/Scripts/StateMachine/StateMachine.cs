using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    IState currentState;
    IState previousState;
    public void ChangeState(IState newstate)
    {
        if(currentState != null) currentState.ExitState();
        previousState = currentState;
        currentState = newstate;
        currentState.LoadState();
    }

    public void RunState()
    {
        currentState.RunState(); ;
    }
    public void ExitState()
    {
        currentState.ExitState();
    }
    public void Input(string input)
    {
        currentState.Input(input);
    }

}
