using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneOneState : MonoBehaviour, IState
{
    GameObject ownerObject;
    private Animator animator;
    public GameObject data;

    private void Start()
    {
        this.ownerObject = this.gameObject;
        animator = GetComponent<Animator>();
    }

    public void ExitState()
    {
        data.GetComponent<Data>().Message = this.gameObject.transform.Find("phone").Find("PhoneMessageCanvas").Find("PhoneMessage").gameObject.GetComponent<Text>().text;
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
