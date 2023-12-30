using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
  /*  PlayerInput playerInput;
    CharacterController characterController;
    Animator anim;

    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 currentRunMovement;
    bool isRunPressed;
    bool isMovementPressed;
    float rotationFactorPerFrame = 20.0f;

    public float walkSpeed;
    public float runSpeed;

    private void Awake()
    {
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        playerInput.CharacterControls.Move.started += OnMovementInput;
        playerInput.CharacterControls.Move.canceled += OnMovementInput;
        playerInput.CharacterControls.Move.performed += OnMovementInput;

        playerInput.CharacterControls.Run.started += OnRun;
        playerInput.CharacterControls.Run.canceled += OnRun;

    }

    void OnMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0; //if movement key is pressed, the number will either be less than or greater than zero
    }

    void OnRun (InputAction.CallbackContext context)
    {
        isRunPressed = context.ReadValueAsButton();
    
    }

    void handleAnimation()
    {
        bool isWalking = anim.GetBool("Walking");
        bool isRunning = anim.GetBool("Running");

        //if move key is being pressed and player is not walking already, start walking
        if(isMovementPressed && !isWalking)
        {
            anim.SetBool("Walking", true);
        }

        //if move key is not being pressed and player is walking, stop walking
        else if(!isMovementPressed && isWalking)
        {
            anim.SetBool("Walking", false);
        }

        //if move key and run key are both pressed and player is not already running, start running
        if ((isMovementPressed && isRunPressed) && !isRunning)
        {
            anim.SetBool("Running", true);
        }

        //if move key or run key stops being pressed, stop running
        else if ((!isMovementPressed || !isRunPressed) && isRunning)
        {
            anim.SetBool("Running", false);
        }
    }

    void handleRotation()
    {
        Vector3 positionToLookAt;
        // the change in position our character should point to
        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = currentMovement.z;

        Quaternion currentRotation = transform.rotation;

        if (isMovementPressed)
        {
            // creates new rotation based on where player is looking
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }
    }

    private void Update()
    {
        handleRotation();
        handleAnimation();

        if (isRunPressed)
        {
            characterController.Move(currentMovement * Time.deltaTime * runSpeed);
        }
        else
        {
            characterController.Move(currentMovement * Time.deltaTime * walkSpeed);
        }

    }

    void HandleGravity()
    {
        if (characterController.isGrounded)
        {
            float groundedGravity = -.05f;
            currentMovement.y = groundedGravity;
            currentRunMovement.y = groundedGravity;
        }
        else
        {
            float gravity = -9.8f;
            currentMovement.y += gravity;
            currentRunMovement.y += gravity;
        }
    }


    Vector3 ConvertToCameraSpace(Vector3 vectorToRotate)
    {

    }
   

    private void OnEnable()
    {
        playerInput.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        playerInput.CharacterControls.Disable();
    }
} */


   // public Inventory inventory; //gives the player an item inventory


    [SerializeField] private UI_Inventory uiInventory;


    public float walkSpeed;
    public float runSpeed;
    public float defaultSpeed;
    public float rotationSpeed;

    public Transform playerTrans;

    public Rigidbody playerRigid;

    float horizontalInput;
    float verticalInput;

    public Transform orientation;

    public bool walking;

    Vector3 moveDirection;

    public float turnSpeed;

    public static Animator anim;
    private CharacterController controller;

    public float degreesPerSecond;

    public static bool isWalking = false;

   // public Transform target; // target so the player faces the direction the camera is pointing when walking


    private void Awake()
    {
       // uiInventory.SetInventory(inventory);
      //  inventory = new Inventory();
    }


    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponentInChildren<Animator>();

        controller = GetComponent<CharacterController>();

        playerRigid = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //the player will move in the direction of the camera based on walkSpeed
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        playerRigid.AddForce(moveDirection.normalized * walkSpeed, ForceMode.Force);

    }

    // Update is called once per frame
    void Update()
    {
        { 
            //sets the axis of horizontal and vertical input
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");

         
        }
        Walking(); //play the function that allows the player to walk

        Turning(); //play the function that allows the player to turn
    }


    public void Walking()
    {

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))


        {

            anim.SetTrigger("Walking"); //plays the walking animation while moving

            float z = Input.GetAxis("Vertical") * Time.deltaTime * walkSpeed; //moves player forward and backwards
            transform.Translate(0f, 0f, z);

            isWalking = true; //the player is walking

        }
        else
        {
            anim.SetTrigger("Idle"); //plays the idle animation
            isWalking = false;
        }


        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) //turn the player to the right
        {
            transform.Rotate(new Vector3(0, degreesPerSecond, 0) * Time.deltaTime);
        }

        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) //turn the player to the left
        {
            transform.Rotate(new Vector3(0, -degreesPerSecond, 0) * Time.deltaTime);
        } 


        if (Input.GetKey(KeyCode.LeftShift)) //increase speed
        {
            // float x2 = Input.GetAxis("Horizontal") * Time.deltaTime * runSpeed; //moves player left and right
            float z2 = Input.GetAxis("Vertical") * Time.deltaTime * runSpeed; //moves player forward and backwards

            // transform.Translate(x2, 0f, 0f);
            transform.Translate(0f, 0f, z2);
        }
    }

    public void Turning()
    {
        //  if (isWalking) //if the player is in motion
        {
            Vector3 Vx = new Vector3(0f, 1f, 0f);
            Vector3 Vy = new Vector3(1f, 0f, 0f);

            //turn player with the track pad
            float xR = Input.GetAxis("Mouse X") * turnSpeed;
            float yR = Input.GetAxis("Mouse Y") * turnSpeed;



            transform.Rotate(Vx, xR * Time.deltaTime);
            //transform.Rotate(Vy, yR * Time.deltaTime);


        }
    }
}

