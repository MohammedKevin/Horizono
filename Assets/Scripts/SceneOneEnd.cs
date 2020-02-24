using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneOneEnd : MonoBehaviour
{
    public StateController stateController;
    public void S1EndEvent()
    {
        stateController.NextState();
    }
}
