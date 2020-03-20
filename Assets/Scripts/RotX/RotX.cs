using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotX
{
    public string encryptText(int selectedNumber, string input)
    {
        string output = "";

        for (int i = 0; i < input.Length; i++)
        {
            if (Char.ToUpper(input[i]) >= 'A' && Char.ToUpper(input[i]) <= 'M')
                output += Convert.ToChar(input[i] + selectedNumber);
            else if (Char.ToUpper(input[i]) >= 'N' && Char.ToUpper(input[i]) <= 'Z')
                output += Convert.ToChar(input[i] - selectedNumber);
            else
                output += input[i];
        }

        return output;
    }
}
