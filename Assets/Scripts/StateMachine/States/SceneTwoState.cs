﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneTwoState : MonoBehaviour, IState
{
    GameObject ownerObject;
    public GameObject data;
    private Animator animator;

    private void Start()
    {
        this.ownerObject = this.gameObject;
        animator = GetComponent<Animator>();
    }

    public void ExitState()
    {
        data.GetComponent<Data>().Message = this.gameObject.transform.Find("Text").Find("TextEnc").gameObject.GetComponent<Text>().text;
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
        Debug.Log("colorInLetter()/in scene 2");
        int rot = this.gameObject.transform.Find("SmallLetterWheel").GetComponent<RotationScript>().input - 'a';
        string currentLetter = this.gameObject.transform.Find("Text").Find("Letter").gameObject.GetComponent<Text>().text;
        if (!(currentLetter.ToLower()[0] >= 'a' && currentLetter.ToLower()[0] <= 'z')) return;
        string currentLetterEnc = RotX.encryptText(rot, currentLetter);
        this.gameObject.transform.Find("BigLetterWheel").Find("Letters").Find(currentLetter.ToUpper()).gameObject.GetComponent<Text>().color = Color.green;
        this.gameObject.transform.Find("SmallLetterWheel").Find("Letters").Find(currentLetterEnc.ToUpper()).gameObject.GetComponent<Text>().color = Color.green;
    }
    public void colorOutLetter()
    {
        Debug.Log("colorOutLetter()/in scene 2");
        int rot = this.gameObject.transform.Find("SmallLetterWheel").GetComponent<RotationScript>().input - 'a';
        string currentLetter = this.gameObject.transform.Find("Text").Find("Letter").gameObject.GetComponent<Text>().text;
        if (!(currentLetter.ToLower()[0] >= 'a' && currentLetter.ToLower()[0] <= 'z')) return;
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

        string letter = this.gameObject.transform.Find("Text").Find("Letter").gameObject.GetComponent<Text>().text;
        animator.SetInteger("LetterParameter", letter.ToUpper()[0] - 'A');

        this.gameObject.transform.Find("Text").Find("TextDec").gameObject.GetComponent<Text>().text = text.Remove(text.Length - 1);
    }
    public void addLetter()
    {
        string letter = this.gameObject.transform.Find("Text").Find("Letter").gameObject.GetComponent<Text>().text;
        this.gameObject.transform.Find("Text").Find("TextEnc").gameObject.GetComponent<Text>().text = letter + this.gameObject.transform.Find("Text").Find("TextEnc").gameObject.GetComponent<Text>().text;
        //Check if Enc is done
        string text = this.gameObject.transform.Find("Text").Find("TextDec").gameObject.GetComponent<Text>().text;
        if (text.Length < 1)
        {
            gameObject.GetComponent<Animator>().SetTrigger("FinishedEnc");
            return;
        }
    }
}
