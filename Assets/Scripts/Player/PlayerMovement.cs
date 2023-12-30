using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed;

   // CameraMove cameraControl;

    private void Awake()
    {
       //cameraControl = CameraMove
    }

    private void Update()
    {
       float horizontal = Input.GetAxis("Horizontal");
       float vertical = Input.GetAxis("Vertical");

       //the direction the player will move
       var moveInput = (new Vector3(horizontal, 0, vertical)).normalized;

       //moveDirection is determined by the camera's horizontal rotation
       var moveDirection = CameraMove.PlanarRotation * moveInput;

       transform.position += moveDirection * Time.deltaTime * walkSpeed;
    }

}

