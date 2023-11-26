using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Inventory inventory; //gives the player an item inventory


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

    public Transform target; // target so the player faces the direction the camera is pointing when walking


    private void Awake()
    {
        uiInventory.SetInventory(inventory);
        inventory = new Inventory();
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
        { /*
            //sets the axis of horizontal and vertical input
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");

            //play the walking animation if pressing the walking keys
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                anim.SetTrigger("Walking");
                anim.ResetTrigger("Idle");
                walking = true;

            }

            //once the walking key is lifted, reset to the idle animation
            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                anim.ResetTrigger("Walking");
                anim.SetTrigger("Idle");
                walking = false;

            }

            /*  if (Input.GetKey(KeyCode.A))
              {
                  playerTrans.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
              }
              if (Input.GetKey(KeyCode.D))
              {
                  playerTrans.Rotate(0, rotationSpeed * Time.deltaTime, 0);
              } 

            //run if the left shift is held down
            if (walking == true)
            {
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {

                    walkSpeed = walkSpeed + runSpeed;
                    anim.SetTrigger("Running");
                    anim.ResetTrigger("Walking");
                }

                //resets the speed and animation once the run key is lifted
                if (Input.GetKeyUp(KeyCode.LeftShift))
                {

                    walkSpeed = defaultSpeed;
                    anim.ResetTrigger("Running");
                    anim.SetTrigger("Walking");
                } */
        }
        Walking(); //play the function that allows the player to walk

        // Turning(); //play the function that allows the player to turn
    }


    public void Walking()
    {

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))


        {

            anim.SetInteger("Movement", 1); //plays the walking animation while moving

            float z = Input.GetAxis("Vertical") * Time.deltaTime * walkSpeed; //moves player forward and backwards
            transform.Translate(0f, 0f, z);

            isWalking = true; //the player is walking

        }
        else
        {
            anim.SetInteger("Movement", 0); //plays the idle animation
            isWalking = false;
        }


      /*  if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) //turn the player to the right
        {
            transform.Rotate(new Vector3(0, degreesPerSecond, 0) * Time.deltaTime);
        }

        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) //turn the player to the left
        {
            transform.Rotate(new Vector3(0, -degreesPerSecond, 0) * Time.deltaTime);
        } */


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
