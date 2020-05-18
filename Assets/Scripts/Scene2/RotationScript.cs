using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour
{
    private double rotateStepSize = 13.84615;
    public char input = 'd';
    private double current = 0;
    private double goal = 0;
    // Start is called before the first frame update
    void Start()
    {
        
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
        double goal = (letter - 'a') * rotateStepSize;
        if (current > goal -0.1 && current < goal + 0.1)
        {
            return;
        }
        this.transform.eulerAngles = new Vector3
        {
            x = this.transform.eulerAngles.x,
            y = this.transform.eulerAngles.y,
            z = this.transform.eulerAngles.z+0.05f
        };
        current = this.transform.eulerAngles.z;
    }
}
