using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneFourState : MonoBehaviour, IState
{
    GameObject ownerObject;
    private Animator animator;

    private void Start()
    {
        this.ownerObject = this.gameObject;
        animator = GetComponent<Animator>();
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
