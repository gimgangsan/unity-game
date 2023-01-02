using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchAndDestroyState : BaseState
{
    protected weaponType weaponScript;

    public SearchAndDestroyState(string name, StateMachine stateMachine, weaponType weaponScript) : base(name, stateMachine)
    {
        this.weaponScript = weaponScript;
    }
}

public class NoTargetState : SearchAndDestroyState
{
    public NoTargetState(TargetFollowerSM stateMachine, weaponType weaponScript)
        : base("NoTargetState", stateMachine, weaponScript) { }

    public override void onStateUpdate()
    {
        if (weaponScript.target != null)
        {
            Debug.Log("transition to followTargetState");
            stateMachine.changeState(((TargetFollowerSM)stateMachine).followTargetState);
            return;
        }
    }
}

public class FollowTargetState : SearchAndDestroyState
{
    float pathUpdateRate = 0.5f;
    float timeElapsed = 0.5f;

    public FollowTargetState(TargetFollowerSM stateMachine, weaponType weaponScript)
        : base("FollowTargetState", stateMachine, weaponScript) { }

    public override void onStateUpdate()
    {
        if (weaponScript.target == null)
        {
            Debug.Log("transition to NoTargetState");
            stateMachine.changeState(((TargetFollowerSM)stateMachine).noTargetState);
            return;
        }
        if (weaponScript.targetInRadius())
        {
            Debug.Log("transition to AttackTargetState");
            stateMachine.changeState(((TargetFollowerSM)stateMachine).attackTargetState);
            return;
        }
        else
        {
            regularlyUpdatePath();
        }
    }

    public void regularlyUpdatePath()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed > pathUpdateRate)
        {
            ((TargetFollowerSM)stateMachine).setDestination(weaponScript.target.position);
            timeElapsed = 0;
        }
    }
}

public class AttackTargetState : SearchAndDestroyState
{

    public AttackTargetState(TargetFollowerSM stateMachine, weaponType weaponScript)
        : base("AttackTargetState", stateMachine, weaponScript) { }

    public override void onStateEnter()
    {
        ((TargetFollowerSM)stateMachine).setDestination(((TargetFollowerSM)stateMachine).transform.position);
    }
    public override void onStateUpdate()
    {
        if (weaponScript.targetInRadius() == false)
        {
            Debug.Log("transition to followTargetState");
            stateMachine.changeState(((TargetFollowerSM)stateMachine).followTargetState);
            return;
        }
        if (weaponScript.readyToFire())
        {
            weaponScript.fire();
        }
    }
}