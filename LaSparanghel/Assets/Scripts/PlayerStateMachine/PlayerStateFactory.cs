using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateFactory
{
    PlayerStateMachine p_context;
    enum PlayerStates { idle, walk, run }
    Dictionary<PlayerStates, PlayerBaseState> l_state = new Dictionary<PlayerStates, PlayerBaseState>();

    public PlayerStateFactory(PlayerStateMachine currentContext)
    {
        p_context = currentContext;
        l_state[PlayerStates.idle] = new PlayerStateIdle(p_context, this);
        l_state[PlayerStates.run] = new PlayerStateRunning(p_context, this);
        l_state[PlayerStates.walk] = new PlayerStateWalking(p_context, this);
    }
    public PlayerBaseState Idle()
    {
        return l_state[PlayerStates.idle];
    }
    public PlayerBaseState Walk()
    {
        return l_state[PlayerStates.walk];
    }
    public PlayerBaseState Run()
    {
        return l_state[PlayerStates.run];
    }
}
