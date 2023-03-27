using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateWalking : PlayerBaseState
{
    public PlayerStateWalking(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {

    }
    public override void EnterState()
    {
        SetSubState(null);
        Context.Animator.SetBool(Context.IsWalkingHash, true);
        Context.Animator.SetBool(Context.IsRunningHash, false);
    }

    public override void UpdateState()
    {
        Context.AppliedMovementX = Context.CurrentMovementX;
        Context.AppliedMovementY = Context.CurrentMovementY;
        CheckSwitchStates();
    }

    public override void ExitState() { }

    public override void CheckSwitchStates()
    {
        if (Context.RunPressed)
        {
            SetSubState(Factory.Run());
        }
        if(!Context.MovementPressed)
        {
            SetSubState(Factory.Idle());
        }    
    }

    public override void InitializeSubState() { }
}
