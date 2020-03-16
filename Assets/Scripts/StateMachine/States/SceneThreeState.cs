using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneThreeState : MonoBehaviour, IState
{
    GameObject ownerObject;

    private void Start()
    {
        this.ownerObject = this.gameObject;
    }

    public void ExitState()
    {
        ownerObject.SetActive(false);
    }

    public void LoadState()
    {
        ownerObject.SetActive(true);
    }

    public void RunState()
    {
    }

    public void Input(string input)
    {

    }
}
