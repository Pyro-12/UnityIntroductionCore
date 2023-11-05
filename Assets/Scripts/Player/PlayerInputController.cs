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

    private void GetReferences()
    {
        // Cambié la inicialización de playerInput para usar el sistema de entrada.
        playerInput = new PlayerInput();
        playerInput.Enable();
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
        // playerInput.Enable(); No necesitas llamarlo aquí, ya se llama en GetReferences.
    }

    private void OnDisable()
    {
        // playerInput.Disable(); No necesitas llamarlo aquí.
        UnsubscribeToDelegates();
    }

    private void UnsubscribeToDelegates()
    {
        playerInput.InGame.Move.started -= OnMove;
        playerInput.InGame.Move.performed -= OnMove;
        playerInput.InGame.Move.canceled -= OnMove;

        playerInput.InGame.Jump.started -= OnJump;
        playerInput.InGame.Jump.performed -= OnJump;
        playerInput.InGame.Jump.canceled -= OnJump;
    }

    public void OnMove(InputAction.CallbackContext value)
    {
        moveDirection = value.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext value)
    {
        Debug.Log("Jump");
        jump = value.ReadValue<float>() > 0.5f;
    }

    public void UpdateJumpState(bool newJumpState)
    {
        jump = newJumpState;
    }

    public Vector2 GetMouseDelta() //Revisar por que no va el joystick R
    {
        return playerInput.InGame.Look.ReadValue<Vector2>();
    }
}