using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;

    public float groundCheckRadius = 0.2f;
    public Vector3 groundCheckOffset;
    public LayerMask groundLayer;

    bool isGrounded;

    Animator anim;

    CharacterController characterController;


    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
       GroundCheck();

       float horizontal = Input.GetAxis("Horizontal");
       float vertical = Input.GetAxis("Vertical");

       float moveAmount = Mathf.Abs (horizontal + vertical); //if either direction is greater than 0, the player is moving
        //abs = absolute! The absolute value!! To account for negatives!

       //the forward direction for the player
       var moveInput = (new Vector3(horizontal, 0, vertical)).normalized;

       //moveDirection is determined by the camera's horizontal rotation
       var moveDirection = CameraMove.PlanarRotation * moveInput;

   

        if (moveAmount > 0) //if the player is moving
        {
            
            //player faces the move direction
            transform.rotation = Quaternion.LookRotation(moveDirection); 

            if (Input.GetKey(KeyCode.LeftShift)) //the player is running
            {
                characterController.Move(moveDirection * runSpeed * Time.deltaTime);
                anim.SetBool("isRunning", true);
            }
            else
            {
                //player move towards the move direction
                characterController.Move(moveDirection * walkSpeed * Time.deltaTime);
                anim.SetBool("isWalking", true);
            }
          
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
    }

    public void GroundCheck()
    {
        //will return true if there are colliders within the sphere created
        isGrounded = Physics.CheckSphere(transform.TransformPoint(groundCheckOffset), groundCheckRadius, groundLayer);

    }

    private void OnDrawGizmosSelected()
    {
       // Gizmos.color = new Color(1, 0, 1, 0.5f);
       // Gizmos.DrawSphere(transform.TransformPoint(groundCheckOffset), groundCheckRadius);
    }

}

