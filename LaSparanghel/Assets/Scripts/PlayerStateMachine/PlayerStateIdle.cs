using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateIdle : PlayerBaseState
{
    public PlayerStateIdle(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
    }
    public override void EnterState()
    {
        SetSubState(null);
        Context.Animator.SetBool(Context.IsWalkingHash, false);
        Context.Animator.SetBool(Context.IsRunningHash, false);
        Context.AppliedMovementX = 0;
        Context.AppliedMovementY = 0;
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void ExitState() { }

    public override void CheckSwitchStates()
    {
        if (Context.MovementPressed && Context.RunPressed)
        {
            SetSubState(Factory.Run());
        }
        else if (Context.MovementPressed)
        {
            SetSubState(Factory.Walk());
        }
    }

    public override void InitializeSubState() 
    {
       if (Context.MovementPressed && !Context.RunPressed)
        {
            SetSubState(Factory.Walk());
        }
        else
        {
            SetSubState(Factory.Run());
        }
    }
}
