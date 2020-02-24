using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    StateMachine stateMachine;
    IState scene1;
    //IState sceneX;
    // Start is called before the first frame update
    void Start() {
        stateMachine = new StateMachine();
        scene1 = GameObject.Find("/SceneOneState").GetComponent<SceneOneState>();
        //sceneX = GameObject.Find("/SceneXState").GetComponent<SceneXState>();
        Debug.Log(scene1);
        stateMachine.ChangeState(scene1); // Select what scene should run in your case szene X
        stateMachine.RunState();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Gets called by the state of the end of the state to change to next state
    void nextState()
    {

    }
}
