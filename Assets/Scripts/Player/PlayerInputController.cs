using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    PlayerInput playerInput;
    private static PlayerInputController instance;
    public static PlayerInputController Instance => instance;
    [HideInInspector] public Vector2 moveDirection;
    [HideInInspector] public bool jump;
    [HideInInspector] public bool dash;
    [HideInInspector] public bool run;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        Cursor.visible = false;
        GetReferences();
    }

    private void Start()
    {
        SubscribeToDelegatesAndUpdateValues();
    }

    private void SubscribeToDelegatesAndUpdateValues()
    {
        playerInput.InGame.Move.started += OnMove;
        playerInput.InGame.Move.performed += OnMove;
        playerInput.InGame.Move.canceled += OnMove;

        playerInput.InGame.Jump.started += OnJump;
        playerInput.InGame.Jump.performed += OnJump;
        playerInput.InGame.Jump.canceled += OnJump;
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    private void UnsubscribeToDelegates()
    {
        playerInput.InGame.Move.started -= OnMove;
        playerInput.InGame.Move.performed -= OnMove;

        playerInput.InGame.Jump.started -= OnJump;
        playerInput.InGame.Jump.performed -= OnJump;
    }

    private void GetReferences()
    {
        playerInput = new PlayerInput();
    }

    public void OnMove(InputAction.CallbackContext value)
    {
        moveDirection = value.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext value)
    {
        JumpInput(value.ReadValue<float>());
    }

    private void JumpInput(float newJumpState)
    {
        jump = newJumpState > 0;
    }

    private void MoveInput(Vector2 newMoveDirection)
    {
        moveDirection = newMoveDirection;
    }

    public Vector2 GetMouseDelta()
    {
        return playerInput.InGame.Look.ReadValue<Vector2>();
    }
}