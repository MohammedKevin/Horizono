using DeepSpace.LaserTracking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ButtonController : MonoBehaviour
{
    private int countA;
    private int countB;
    private int countC;
    private int countD;

    public int CountA
    {
        get { return countA; }
    }

    public int CountB
    {
        get { return countB; }
    }
    public int CountC
    {
        get { return countC; }
    }

    public int CountD
    {
        get { return countD; }
    }

    /// <summary>
    /// Adds an Entity to a specific list.
    /// </summary>
    /// <param name="btn">1 = btn_A, 2 = btn_B, 3 = btn_C, 4 = btn_D</param>
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
            case 3:
                countC++;
                break;
            case 4:
                countD++;
                break;
        }
    }

    /// <summary>
    /// Removes an Entity from a specific list. If there is nothing in the list, nothing will be removed.
    /// </summary>
    /// <param name="btn">1 = btn_A, 2 = btn_B, 3 = btn_C, 4 = btn_D</param>
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
            case 3:
                if (countC > 0)
                    countC--;
                break;
            case 4:
                if (countD > 0)
                    countD--;
                break;
        }
    }

    /// <summary>
    /// Returns the index of the button with the most person standing on it. 1 = btn_A, 2 = btn_B, 3 = btn_C, 4 = btn_D
    /// </summary>
    /// <returns></returns>
    public int GetMax()
    {
        int[] arr = { CountA, CountB, CountC, CountD };
        return arr.ToList().IndexOf(arr.Max()) + 1;
    }
}
