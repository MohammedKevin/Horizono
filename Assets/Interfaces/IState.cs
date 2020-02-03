using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void LoadState();
    void RunState();
    void ExitState();
    void Input(string input);
}
