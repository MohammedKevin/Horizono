using DeepSpace.LaserTracking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    private int countA;
    private int countB;

    public int CountA
    {
        get { return countA; }
    }

    public int CountB
    {
        get { return countB; }
    }

    public void AddEntity(int btn)
    {
        switch (btn)
        {
            case 1:
                countA++;
                break;
            case 2:
                countB++;
                break;
        }
    }

    public void RemoveEntity(int btn)
    {
        switch (btn)
        {
            case 1:
                if (countA > 0)
                    countA--;
                break;
            case 2:
                if (countB > 0)
                    countB--;
                break;
        }
    }
}
