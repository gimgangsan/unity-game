using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollowerSM : StateMachine
{
    [HideInInspector] public NoTargetState noTargetState;
    [HideInInspector] public FollowTargetState followTargetState;
    [HideInInspector] public AttackTargetState attackTargetState;

    weaponType weaponScript;
    pathFinding pathFinding;

    public GameObject weaponTesting;

    private void Awake()
    {
        pathFinding = gameObject.GetComponent<pathFinding>();
        weaponScript = weaponTesting.GetComponent<weaponType>();
        noTargetState = new NoTargetState(this, weaponScript);
        followTargetState = new FollowTargetState(this, weaponScript);
        attackTargetState = new AttackTargetState(this, weaponScript);
    }
    void Start()
    {
        currentState = getInitialState();
        if (currentState != null)
        {
            currentState.onStateEnter();
        }
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.onStateUpdate();
        }
    }

    protected override BaseState getInitialState()
    {
        return noTargetState;
    }

    public void setDestination(Vector2 desiredPosition)
    {
        pathFinding.setDesiredPosition(desiredPosition);
    }

    public void setAttackTarget(Transform targetTransform)
    {
        weaponScript.setTarget(targetTransform);
    }
}
