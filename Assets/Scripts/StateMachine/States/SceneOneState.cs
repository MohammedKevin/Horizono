using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneOneState : MonoBehaviour, IState
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
        animator.SetBool("IsReadyToMoveWall", true);
        
    }

    public void Input(string input)
    {
        
    }
}
