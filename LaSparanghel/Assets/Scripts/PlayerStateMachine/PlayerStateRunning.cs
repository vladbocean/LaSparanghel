using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateRunning : PlayerBaseState
{
    public PlayerStateRunning(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {

    }
    public override void EnterState()
    {
        SetSubState(null);
        Context.Animator.SetBool(Context.IsWalkingHash, true);
        Context.Animator.SetBool(Context.IsRunningHash, true);
    }

    public override void UpdateState()
    {
        Context.AppliedMovementX = Context.CurrentMovementX * Context.runFactor;
        Context.AppliedMovementY = Context.CurrentMovementY * Context.runFactor;
        CheckSwitchStates();
    }

    public override void ExitState() { }

    public override void CheckSwitchStates()
    {
        if (Context.MovementPressed && !Context.RunPressed)
        {
            SetSubState(Factory.Walk());
        }
        if(!Context.MovementPressed && !Context.RunPressed)
        {
            SetSubState(Factory.Idle());
        }
    }

    public override void InitializeSubState() { }
}
