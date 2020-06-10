using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneOneState : MonoBehaviour, IState
{
    GameObject ownerObject;
    private Animator animator;
    public GameObject test;

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
        //RotX.decryptText(1, RotX.encryptText(1, "aBcDeF1hijklmnopqrstuvwxyz"));
        
    }

    public void Input(string input)
    {
        
    }
}
