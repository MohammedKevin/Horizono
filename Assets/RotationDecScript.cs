using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class RotationDecScript : MonoBehaviour
{
    private double rotateStepSize = 13.84615;
    char rotChar;
    private double current = 0;
    public GameObject data;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void calcDecLetter()
    {
        char encLetter = data.GetComponent<Data>().encLetter;
        int dif = encLetter - 'a';
        int decNumber = 26 - dif;
        char decChar = Convert.ToChar('a' + decNumber);
        rotChar = decChar;
        data.GetComponent<Data>().encLetter = decChar;
    }

    // Update is called once per frame
    void Update()
    {
        Rotate(rotChar);
        Rotate(rotChar);
        Rotate(rotChar);
        Rotate(rotChar);
        Rotate(rotChar);
    }
    void Rotate(char letter)
    {
        if (rotChar == null)
        {
            return;
        }
        double goal = (letter - 'a') * rotateStepSize;
        if (current > goal - 0.1 && current < goal + 0.1)
        {
            //needs to be thrown after 5 secs from colision
            gameObject.GetComponentInParent<Animator>().SetTrigger("StartMoveLetter");
            data.GetComponent<Data>().encLetter = rotChar;
            return;
        }
        this.transform.eulerAngles = new Vector3
        {
            x = this.transform.eulerAngles.x,
            y = this.transform.eulerAngles.y,
            z = this.transform.eulerAngles.z + 0.05f
        };
        current = this.transform.eulerAngles.z;
    }
}
