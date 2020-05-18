using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneTwoState : MonoBehaviour, IState
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

    public void colorInLetter()
    {
        string currentLetter = this.gameObject.transform.Find("Text").Find("Letter").gameObject.GetComponent<Text>().text;
        this.gameObject.transform.Find("BigLetterWheel").Find("Letters").Find(currentLetter).gameObject.GetComponent<Text>().color = Color.green;
    }
    public void colorOutLetter()
    {
        string currentLetter = this.gameObject.transform.Find("Text").Find("Letter").gameObject.GetComponent<Text>().text;
        this.gameObject.transform.Find("BigLetterWheel").Find("Letters").Find(currentLetter).gameObject.GetComponent<Text>().color = Color.white;
    }
}
