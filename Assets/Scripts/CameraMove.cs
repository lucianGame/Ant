using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    float _horizontalInput;
    float _verticalInput;
    Vector3 _playerInput;
    [SerializeField] CharacterController _characterController;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        //get and store player input every frame
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");

        //set it to the X and Z values of the Vector3
        _playerInput.x = _horizontalInput;
        _playerInput.y = _verticalInput;

        //transform position using Move and player input Vector3
        _characterController.Move(_playerInput * Time.deltaTime);
    }

    /*  //REFERENCES
      public Transform orientation;
      public Transform player;
      public Transform playerObj;
      public Rigidbody body;

      //VARIABLES
      public float rotationSpeed;

      private void Start()
      {
          Cursor.lockState = CursorLockMode.Locked;
          Cursor.visible = false;
      } 

      void Update()
      {
          //rotation orientation
          Vector3 viewDirection = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
          orientation.forward = viewDirection.normalized;

          //rotate player object
          float horizontalInput = Input.GetAxis("Horizontal");
          float verticalInput = Input.GetAxis("Vertical");
          Vector3 inputDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

          if (inputDirection != Vector3.zero)
          {
              playerObj.forward = Vector3.Slerp(playerObj.forward, inputDirection.normalized, Time.deltaTime * rotationSpeed);
          }
      }
    */
}


