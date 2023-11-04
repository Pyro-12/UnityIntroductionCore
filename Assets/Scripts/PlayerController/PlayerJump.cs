using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    private Vector3 playerVelocity;
    [SerializeField] float jumpForce;
    [SerializeField] bool isGrounded;
    [SerializeField] LayerMask GroundLayer;
    private bool jumpPressed = false;
    private float gravity = -9.81f;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        MovementJump();
    }

    void MovementJump()
    {

        /*  void OnJump(InputValue value)
          {
              Debug.Log("jump");

              //check if there's not vert jump
              if (moveDirection.y == 0)
              {
                  jumpPressed = true;
                  Debug.Log("Can jump");
              }

              else
              {
                  Debug.Log("Can't jump");
              }

          }*/
    }
}
