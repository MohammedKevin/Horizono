using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotX
{
    public static string encryptText(int selectedNumber, string input)
    {
        string output = "";

        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] >= 'A' && input[i] <= 'Z')
            {
                int x = input[i] - 'A';
                x += selectedNumber;
                x = x % 26;
                output += Convert.ToChar('A' + x);
            }
            else if(input[i] >= 'a' && input[i] <= 'z')
            {
                int x = input[i] - 'a';
                x += selectedNumber;
                x = x % 26;
                output += Convert.ToChar('a' + x);
            }
            else
            {
                output += "?";
            }
        }
        return output;
    }
    public static string decryptText(int selecetedNumber, string input)
    {
        return encryptText(26 - selecetedNumber, input);
    }
}
