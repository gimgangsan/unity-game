using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    protected BaseState currentState;

    // Start is called before the first frame update
    void Start()
    {
        currentState = getInitialState();
        if(currentState != null )
        {
            currentState.onStateEnter();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(currentState != null)
        {
            currentState.onStateUpdate();
        }
    }

    public void changeState(BaseState newState)
    {
        currentState.onStateExit();
        currentState = newState;
        newState.onStateEnter();
    }

    protected virtual BaseState getInitialState()
    {
        return null;
    }
}
