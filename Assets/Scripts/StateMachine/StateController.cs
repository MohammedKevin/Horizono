using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateController : MonoBehaviour
{
    StateMachine stateMachine;
    IState scene1;
    LinkedList<IState> scenes = new LinkedList<IState>();
    int currentState = 0;
    //IState sceneX;
    // Start is called before the first frame update
    void Start() {
        stateMachine = new StateMachine();
        getStates();
        NextState();
        stateMachine.RunState();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Gets called by the state of the end of the state to change to next state
    public void NextState()
    {
        stateMachine.ChangeState(scenes.ElementAt(currentState));
        currentState++;
    }
    public void getStates()
    {
        string[] stateNames = { /*"/SceneOneState" , "/SceneTwoState",*/ "/SceneThreeState" };
        foreach (var sceneName in stateNames)
        {
             scenes.AddLast(GameObject.Find(sceneName).GetComponent<IState>());
        }
        foreach (var scene in scenes)
        {
            scene.ExitState();
        }
    }
}
