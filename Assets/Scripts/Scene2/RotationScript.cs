using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour
{
    private int rotateStepSize = 360 / 26;
    public char input = 'a';
    private bool isRotating = false;
    private float current = 0;
    private float goal = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        current = this.transform.eulerAngles.y;
        Rotate(input);
    }
    void Rotate(char letter)
    {
        int rotation = (letter - 'a') * rotateStepSize;
        goal = rotation;
        if (current > goal -2 && current < goal + 2)
        {
            isRotating = false;
            return;
        }
        this.transform.eulerAngles = new Vector3
        {
            x = this.transform.eulerAngles.x,
            y = this.transform.eulerAngles.y + 1,
            z = this.transform.eulerAngles.z
        };
        current = this.transform.eulerAngles.y;
    }
}
