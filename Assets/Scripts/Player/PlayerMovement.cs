
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement: MonoBehaviour
{
    #region Data
    [Header("Player")]
    [SerializeField] float speed;
    [SerializeField] [Range(0.0f, 10f)]  float sprint = 10f;
    [SerializeField] [Range(0.0f, 0.3f)] float rotationSmooth= 0.05f; // TO-DO rotacion en mov
    private Vector2 moveDirection;

    [Space(10)] 
    [Header("Jump")]
    [SerializeField] float gravity = -9.81f; //stiamted, unity use his own gravity system
    [SerializeField] float jumpForce;

    [Space(10)] [Header("Player is grounded")] [Tooltip ("Scans the assignated layer in order to create the jump action")] [SerializeField] LayerMask GroundLayer;
    #endregion

    #region Auxiliary data
    //AUXILIARY DATA 
    private float playerVelocity;
    PlayerInputController playerInputController;
    private float targetRotation; 
    [SerializeField] bool isGrounded;
    private bool jumpPressed = false;
    private float verticalVelocity;
    #endregion
    #region Initialize script
    private void Awake()
    {
        playerInputController = PlayerInputController.Instance;
    }
    #endregion
    #region Script's logic

    //GOALS-> create: sprint, normalize inputs for deaccelerate, jump mechanic, camera rotation
    void Update()
    {
        Move();
        Jump();
    }
    void Move()
    {
        // Utiliza las acciones del PlayerInputController
        Vector3 move = new Vector3(playerInputController.moveDirection.x, 0, playerInputController.moveDirection.y).normalized;

        // Rotate when there's player input movement
        if (move != Vector3.zero)
        {
            targetRotation = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg;

            // Rotate the player towards the target direction smoothly
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref playerVelocity, rotationSmooth);
            transform.eulerAngles = new Vector3(0, rotation, 0);
            // rotate relative to camera position
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }

        Vector3 targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;
        Vector3 newPosition = transform.position + (new Vector3(move.x, verticalVelocity, move.z) * Time.deltaTime);
        // Set the new input player position
        transform.position = newPosition;
        moveDirection = playerInputController.moveDirection;
    }
    void Jump()
    {
        if (isGrounded)
        {
            if (playerVelocity < 0.0f)
            {
                playerVelocity = -5f;
            }
        }

        if (jumpPressed && isGrounded)
        {
            // Adjust vertical Velocity for jump simulation
            verticalVelocity = Mathf.Sqrt(-2 * gravity * jumpForce); // jumpForce es la fuerza del salto 
            jumpPressed = true;
        }
        else
        {
              jumpPressed = false;//->not grounded, don't jump. Ver como acceder a los inputs
        }
    }

  
    #region Inputs messages
    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log("Move input received: " + moveDirection);
        moveDirection = context.ReadValue<Vector2>();
    }
    void OnJump(InputValue value)
    {
        Debug.Log("buttonpressed");
        jumpPressed = value.Get<float>() > 0.5f;
    }
    #endregion
    #endregion

}
