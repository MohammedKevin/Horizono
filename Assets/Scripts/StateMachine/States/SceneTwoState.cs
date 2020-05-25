using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneTwoState : MonoBehaviour, IState
{
    GameObject ownerObject;
    int currentX = 10;

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
        int rot = this.gameObject.transform.Find("SmallLetterWheel").GetComponent<RotationScript>().input - 'a';
        string currentLetter = this.gameObject.transform.Find("Text").Find("Letter").gameObject.GetComponent<Text>().text;
        string currentLetterEnc = RotX.encryptText(rot, currentLetter);
        this.gameObject.transform.Find("BigLetterWheel").Find("Letters").Find(currentLetter).gameObject.GetComponent<Text>().color = Color.green;
        this.gameObject.transform.Find("SmallLetterWheel").Find("Letters").Find(currentLetterEnc).gameObject.GetComponent<Text>().color = Color.green;
    }
    public void colorOutLetter()
    {
        int rot = this.gameObject.transform.Find("SmallLetterWheel").GetComponent<RotationScript>().input - 'a';
        string currentLetter = this.gameObject.transform.Find("Text").Find("Letter").gameObject.GetComponent<Text>().text;
        string currentLetterEnc = RotX.encryptText(rot, currentLetter);
        this.gameObject.transform.Find("BigLetterWheel").Find("Letters").Find(currentLetter).gameObject.GetComponent<Text>().color = Color.white;
        this.gameObject.transform.Find("SmallLetterWheel").Find("Letters").Find(currentLetterEnc).gameObject.GetComponent<Text>().color = Color.white;
        this.gameObject.transform.Find("Text").Find("Letter").gameObject.GetComponent<Text>().text = currentLetterEnc;
    }
}
