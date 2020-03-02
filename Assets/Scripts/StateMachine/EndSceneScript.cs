using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSceneScript : MonoBehaviour
{
    public StateController stateController;
    public void EndSceneEvent()
    {
        stateController.NextState();
    }
}