using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{

    Animator p_animator;

    int p_isWalkingHash;
    int p_isRunningHash;

    PlayerInput p_playerInput;
    CharacterController p_characterController;

    //movement variables
    Vector2 p_currentMovementInput;
    Vector2 p_currentMovement;
    Vector2 p_appliedMovement;
    bool p_movementPressed;
    bool p_runPressed;
    public float runFactor = 3.0f;

    //rotation variables
    public float rotationFactor = 15.0f;


    PlayerBaseState p_currentState;
    PlayerStateFactory p_states;

    //getters and setters
    public PlayerBaseState CurrentState { get { return p_currentState; } set { p_currentState = value; } }
    public Animator Animator { get { return p_animator; } }
    public int IsWalkingHash { get { return p_isWalkingHash; } }
    public int IsRunningHash { get { return p_isRunningHash; } }
    public bool RunPressed { get { return p_runPressed; } }
    public bool MovementPressed { get { return p_movementPressed; } }
    public float CurrentMovementY { get { return p_currentMovement.y; } set { p_currentMovement.y = value; } }
    public float CurrentMovementX { get { return p_currentMovement.x; } set { p_currentMovement.x = value; } }
    public float AppliedMovementY { get { return p_appliedMovement.y; } set { p_appliedMovement.y = value; } }
    public float AppliedMovementX { get { return p_appliedMovement.x; } set { p_appliedMovement.x = value; } }



    void Awake() // called earlier than start
    {
        p_playerInput = new PlayerInput();
        p_characterController = GetComponent<CharacterController>();
        p_animator = GetComponent<Animator>();

        p_states = new PlayerStateFactory(this);
        p_currentState = p_states.Idle();
        p_currentState.EnterState();

        p_isWalkingHash = Animator.StringToHash("isWalking");
        p_isRunningHash = Animator.StringToHash("isRunning");

        //movement callbacks
        p_playerInput.CharacterControls.Movement.started += OnMovementInput;
        p_playerInput.CharacterControls.Movement.performed += OnMovementInput;
        p_playerInput.CharacterControls.Movement.canceled += OnMovementInput;
        //run callbacks
        p_playerInput.CharacterControls.Run.started += OnRun;
        p_playerInput.CharacterControls.Run.canceled += OnRun;


    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //HandleRotation();

        p_currentState.UpdateStates();

        p_characterController.Move(p_appliedMovement * Time.deltaTime);

    }

    void OnRun(InputAction.CallbackContext context)
    {
        p_runPressed = context.ReadValueAsButton();
    }
    void OnMovementInput(InputAction.CallbackContext context)
    {
        p_currentMovementInput = context.ReadValue<Vector2>();
        p_currentMovement.x = p_currentMovementInput.x;
        p_currentMovement.y = p_currentMovementInput.y;
        p_movementPressed = p_currentMovementInput.x != 0 || p_currentMovementInput.y != 0;
    }
    void OnEnable()
    {
        p_playerInput.CharacterControls.Enable();
    }
    void OnDisable()
    {
        p_playerInput.CharacterControls.Disable();
    }
    void HandleRotation()
    {
        Vector2 l_positionToLookAt;
        l_positionToLookAt.x = p_currentMovement.x;
        l_positionToLookAt.y = p_currentMovement.y;
        Quaternion l_currentRotation = transform.rotation;
        if (p_movementPressed)
        {
            Quaternion l_targetRotation = Quaternion.LookRotation(l_positionToLookAt);
            transform.rotation = Quaternion.Slerp(l_currentRotation, l_targetRotation, Time.deltaTime * rotationFactor);
        }
    }
}
