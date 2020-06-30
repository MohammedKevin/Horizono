using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneTwoState : MonoBehaviour, IState
{
    GameObject ownerObject;
    public GameObject data;

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
        this.gameObject.transform.Find("Text").Find("TextDec").gameObject.GetComponent<Text>().text = data.GetComponent<Data>().Message;
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
        this.gameObject.transform.Find("BigLetterWheel").Find("Letters").Find(currentLetter.ToUpper()).gameObject.GetComponent<Text>().color = Color.green;
        this.gameObject.transform.Find("SmallLetterWheel").Find("Letters").Find(currentLetterEnc.ToUpper()).gameObject.GetComponent<Text>().color = Color.green;
    }
    public void colorOutLetter()
    {
        int rot = this.gameObject.transform.Find("SmallLetterWheel").GetComponent<RotationScript>().input - 'a';
        string currentLetter = this.gameObject.transform.Find("Text").Find("Letter").gameObject.GetComponent<Text>().text;
        string currentLetterEnc = RotX.encryptText(rot, currentLetter);
        this.gameObject.transform.Find("BigLetterWheel").Find("Letters").Find(currentLetter.ToUpper()).gameObject.GetComponent<Text>().color = Color.white;
        this.gameObject.transform.Find("SmallLetterWheel").Find("Letters").Find(currentLetterEnc.ToUpper()).gameObject.GetComponent<Text>().color = Color.white;
        this.gameObject.transform.Find("Text").Find("Letter").gameObject.GetComponent<Text>().text = currentLetterEnc;
    }
    public void removeLetter()
    {
        string text = this.gameObject.transform.Find("Text").Find("TextDec").gameObject.GetComponent<Text>().text;
        if (text.Length < 1) return;
        this.gameObject.transform.Find("Text").Find("Letter").gameObject.GetComponent<Text>().text = text[text.Length - 1].ToString();
        this.gameObject.transform.Find("Text").Find("TextDec").gameObject.GetComponent<Text>().text = text.Remove(text.Length - 1);
    }
    public void addLetter()
    {
        Debug.Log("AddLetter()");
        this.gameObject.transform.Find("Text").Find("TextEnc").gameObject.GetComponent<Text>().text = this.gameObject.transform.Find("Text").Find("Letter").gameObject.GetComponent<Text>().text + this.gameObject.transform.Find("Text").Find("TextEnc").gameObject.GetComponent<Text>().text;
        
        //Check if Enc is done
        string text = this.gameObject.transform.Find("Text").Find("TextDec").gameObject.GetComponent<Text>().text;
        if (text.Length < 1)
        {
            gameObject.GetComponent<Animator>().SetTrigger("FinishedEnc");
            gameObject.GetComponent<Animator>().SetBool("EncFinished", true);
            return;
        }
    }
}
