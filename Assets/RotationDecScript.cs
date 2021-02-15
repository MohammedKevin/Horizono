using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class RotationDecScript : MonoBehaviour
{
    private double rotateStepSize = 13.84615;
    public char input;
    private double current = 0;
    public GameObject data;

    // Start is called before the first frame update
    void Start()
    {
        input = Convert.ToChar('a' + (26 - (data.GetComponent<Data>().encLetter - 'a')));
    }

    // Update is called once per frame
    void Update()
    {
        Rotate(input);
        Rotate(input);
        Rotate(input);
        Rotate(input);
        Rotate(input);
    }
    void Rotate(char letter)
    {
        if (input == null)
        {
            return;
        }
        double goal = (letter - 'a') * rotateStepSize;
        if (current > goal - 0.1 && current < goal + 0.1)
        {
            //needs to be thrown after 5 secs from colision
            gameObject.GetComponentInParent<Animator>().SetTrigger("StartMoveLetter");
            data.GetComponent<Data>().encLetter = input;
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
