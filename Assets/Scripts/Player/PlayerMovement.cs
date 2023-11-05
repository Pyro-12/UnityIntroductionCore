
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
    [SerializeField] public float GroundedOffset = -0.14f;
    [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")][SerializeField] public float GroundedRadius = 0.28f;
    #endregion

    #region Auxiliary data
    //AUXILIARY DATA 
    private float playerVelocity;
    [SerializeField]PlayerInputController playerInputController;
    private float targetRotation; 
    [SerializeField] bool isGrounded;
    private bool jumpPressed = false;
    private float verticalVelocity;
    #endregion
    #region Initialize script
    private void Awake()
    {
        GetReferences();
    }

    void GetReferences()
    {
        // Intenta encontrar un objeto con el script PlayerInputController adjunto.
        playerInputController = FindObjectOfType<PlayerInputController>();

        // Asegúrate de que playerInputController no sea nulo después de la búsqueda.
        if (playerInputController == null)
        {
            Debug.LogError("PlayerInputController not found in the scene. Make sure it is attached to an object.");
        }
    }
    #endregion


    #region Script's logic
    void Update()
    {
        GroundCheck();
        Move();
        Jump();
    }

    void Move()
    {
        // Verifica si playerInputController es nulo antes de usarlo.
        if (playerInputController != null)
        {
            Vector3 move = new Vector3(playerInputController.moveDirection.x, 0, playerInputController.moveDirection.y).normalized;

            if (move != Vector3.zero)
            {
                targetRotation = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg;

                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref playerVelocity, rotationSmooth);
                transform.eulerAngles = new Vector3(0, rotation, 0);
                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }

            Vector3 targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;
            Vector3 newPosition = transform.position + (new Vector3(move.x, verticalVelocity, move.z) * Time.deltaTime);
            transform.position = newPosition;
            moveDirection = playerInputController.moveDirection;
        }
        else
        {
            Debug.LogError("PlayerInputController is null. Make sure it is properly initialized.");
        }
    }
    void GroundCheck()
    {
        // set sphere position, with offset
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
        isGrounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayer, QueryTriggerInteraction.Ignore);
        // if (_hasAnimator)
        // {
        //     _animator.SetBool(_animIDOnGround, Grounded);
        // }
    }
    void Jump()
    {
        if (isGrounded)
        {// stop our velocity dropping when grounded
            if (playerVelocity < 0.0f)
            {
                playerVelocity = -5f;
            }
            //Jump
            if (jumpPressed)
            {

                verticalVelocity = Mathf.Sqrt(jumpForce * -2f * gravity);
            }
        }

        else
        {

            // if we are not grounded, do not jump
            playerInputController.jump = false;
        }
    }

  
    #region Inputs messages
  /*  public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log("Move input received: " + moveDirection);
        moveDirection = context.ReadValue<Vector2>();
    }
    void OnJump(InputValue value)
    {
        Debug.Log("buttonpressed");
        jumpPressed = value.Get<float>() > 0.5f;
    }*/
    #endregion
    #endregion

}
