public abstract class PlayerBaseState
{
    private bool p_isRootState = false;
    private PlayerStateMachine context;
    private PlayerStateFactory factory;
    private PlayerBaseState p_currentSubState;
    private PlayerBaseState p_currentSuperState;

    protected bool IsRootState { get { return p_isRootState; } set { p_isRootState = value; } }
    protected PlayerStateFactory Factory { get { return factory; } }
    protected PlayerStateMachine Context { get { return context; } }
    public PlayerBaseState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    {
        context = currentContext;
        factory = playerStateFactory;
    }
    public abstract void EnterState();

    public abstract void UpdateState();

    public abstract void ExitState();

    public abstract void CheckSwitchStates();

    public abstract void InitializeSubState();

    public void UpdateStates()
    {
        UpdateState();
        if (p_currentSubState != null)
        {
            p_currentSubState.UpdateStates();
        }
    }

    protected void SwitchState(PlayerBaseState newState)
    {
        ExitState();

        newState.EnterState();

        if (p_isRootState)
        {
            context.CurrentState = newState;
        }
        else if (p_currentSuperState != null)
        {
            p_currentSuperState.SetSubState(newState);
        }


    }

    protected void SetSuperState(PlayerBaseState newSuperState)
    {
        p_currentSuperState = newSuperState;
    }

    protected void SetSubState(PlayerBaseState newSubState)
    {
        p_currentSubState = newSubState;
        p_currentSubState.EnterState();
        newSubState.SetSuperState(this);
    }

}
