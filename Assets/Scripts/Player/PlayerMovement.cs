
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
    PlayerInput playerInput;
    private float targetRotation; 
    [SerializeField] bool isGrounded;
    private bool jumpPressed = false;
    private float verticalVelocity;
    #endregion
    #region Initialize script
    void Start()
    {

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
       // if (moveDirection == Vector2.zero) targetSpeed = 0;
      
        Vector3 move = new Vector3(moveDirection.x, 0, moveDirection.y).normalized;

        // Rotate when there's player input movement
         if (moveDirection != Vector2.zero)
         {
             targetRotation = Mathf.Atan2(moveDirection.x, moveDirection.y) * Mathf.Rad2Deg;

             // Rotate the player towards the target direction smoothly
             float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref playerVelocity, rotationSmooth);
             transform.eulerAngles = new Vector3(0, rotation, 0);
             // rotate relative to camera position
             transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
         }

         Vector3 targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;
         Vector3 newPosition = transform.position + (new Vector3(moveDirection.x, verticalVelocity, moveDirection.y) * Time.deltaTime);
         // Set the new input player position
         transform.position = newPosition;
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

  /*  void RotateCamera ()
    {
        Vector3 move = new Vector3(moveDirection.x, 0, moveDirection.y).normalized;

        // Rotar el jugador solo si hay movimiento
        if (move.magnitude > 0.0f)
        {
            targetRotation = Mathf.Atan2(moveDirection.x, moveDirection.y) * Mathf.Rad2Deg;
        }

        Vector3 targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;
        Vector3 newPosition = transform.position + (targetDirection * speed * Time.deltaTime);

        // Añadir sistema de gravedad al movimiento del jugador
        verticalVelocity += gravity * Time.deltaTime;

        // Ajustar nueva posición
        newPosition.y += verticalVelocity * Time.deltaTime;

        // Rotar el jugador hacia la dirección objetivo suavemente
        float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref playerVelocity, rotationSmooth);
        transform.eulerAngles = new Vector3(0, rotation, 0);
    }*/ //con este sistema solo se bloquea el jugador para mirar alrededor. Revisar para ajustar a la camara

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
