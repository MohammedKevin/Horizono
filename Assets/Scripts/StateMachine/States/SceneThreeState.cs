using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneThreeState : MonoBehaviour, IState
{
    GameObject ownerObject;
    SceneFiveState scene5;

    private void Start()
    {
        this.ownerObject = this.gameObject;
        //scene5 =  GameObject.Find("SceneFiveState").GetComponent<SceneFiveState>();

    }

    public void ExitState()
    {
        ownerObject.SetActive(false);
    }

    public void LoadState()
    {
        ownerObject.SetActive(true);
        //scene5.LoadState();
    }

    public void RunState()
    {
    }

    public void Input(string input)
    {

    }
}
