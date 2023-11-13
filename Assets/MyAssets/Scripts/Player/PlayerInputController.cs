using UnityEngine;
using System;
using UnityEngine.InputSystem;
[RequireComponent(typeof(PlayerInput))]
public class PlayerInputController : MonoBehaviour
{

    PlayerInput playerInput;
    [HideInInspector] public Vector2 moveDirection;
    [HideInInspector] public bool jump;
    [HideInInspector] public bool dash;
    [HideInInspector] public bool run;
    [HideInInspector] public bool sprint;
    [HideInInspector] public bool shoot;


    public event Action<InputAction.CallbackContext> OnShoot;

    private void Awake()
    {
        Cursor.visible = false;
        GetReferences();
    }
    private void OnEnable()
    {
        playerInput = new PlayerInput();
         playerInput.InGame.Enable();
        SubscribeToDelegatesAndUpdateValues();
    }

    private void GetReferences()
    {
        //playerInput = new PlayerInput();
       
        //SubscribeToDelegatesAndUpdateValues();
    }


    private void SubscribeToDelegatesAndUpdateValues()
    {
        playerInput.InGame.Move.started += OnMove;
        playerInput.InGame.Move.performed += OnMove;
        playerInput.InGame.Move.canceled += OnMove;

        playerInput.InGame.Jump.started += OnJump;
        playerInput.InGame.Jump.canceled += OnJump;

        playerInput.InGame.Sprint.performed += OnSprint;
        playerInput.InGame.Sprint.canceled += OnSprint;

        playerInput.InGame.Fire.performed += OnFire; 
        playerInput.InGame.Fire.canceled += OnFire;
    }

    public void OnMove(InputAction.CallbackContext value)
    {
        moveDirection = value.ReadValue<Vector2>();
        //Debug.Log("Move Direction: " + moveDirection);
    }

    public void OnJump(InputAction.CallbackContext value)
    {
        JumpState(value.ReadValue<float>());
        Debug.Log("Jump: " + jump);
    }

    public void OnSprint(InputAction.CallbackContext value)
    {
        //(Debug.Log("Sprint input detected: " + value.ReadValue<float>());
        sprint = value.ReadValue<float>() > 0;
    }

    public void OnDash()
    {

    }

    public void OnRun ()
    {

    }

    public void OnFire(InputAction.CallbackContext value)
    {
        if (value.ReadValueAsButton())
        {
            Debug.Log("Botón de disparo presionado");
            // Invocar el evento OnShoot
            OnShoot?.Invoke(value);
        }
    }
    void JumpState(float newJumpState)
    {
        if (newJumpState > 0)
        {
            
            jump = true;
        }
        else
        {
            jump = false;
        }
    }

    public bool IsSprinting()
    {
        return sprint;
    }

    private void UnsubscribeToDelegates()
    {
        playerInput.InGame.Move.started -= OnMove;
        playerInput.InGame.Move.performed -= OnMove;
        playerInput.InGame.Move.canceled -= OnMove;

        playerInput.InGame.Jump.started -= OnJump;
        playerInput.InGame.Jump.canceled -= OnJump;

        playerInput.InGame.Sprint.performed -= OnSprint;
        playerInput.InGame.Sprint.canceled -= OnSprint;

        playerInput.InGame.Fire.performed -= OnFire;
        playerInput.InGame.Fire.canceled -= OnFire;
    }

    public Vector2 GetMouseDelta() 
    {
        return playerInput.InGame.Look.ReadValue<Vector2>();
    }

    private void OnDeviceLost(InputDevice device)
    {
        Debug.LogWarning("Device lost: " + device);
        // Realizar acciones específicas cuando se pierde el dispositivo, si es necesario.
    }
}