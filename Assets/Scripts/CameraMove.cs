using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform followTarget;

    public float cameraDistanceZ = 12.0f;
    public float cameraDistanceY = -3.0f;

    public float minVerAngle = -45f;
    public float maxVerAngle = 45f;

    float rotationY;
    float rotationX;

    public float cameraSpeed = 3f;

    public bool invertX;
    public bool invertY;

    float invertXVal;
    float invertYVal;


    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    private void Update()
    {
        //if invert camera rotation is true, multiply the rotation values by -1
        invertXVal = (invertX) ? -1 : 1;
        invertYVal = (invertY) ? -1 : 1;

        //camera rotation is determined by trackpad
        rotationY += Input.GetAxis("Mouse X") * invertXVal * cameraSpeed;
        rotationX += Input.GetAxis("Mouse Y") * invertYVal * cameraSpeed;
        //limit the vertical rotation
        rotationX = Mathf.Clamp(rotationX, minVerAngle, maxVerAngle);

        var targetRotation = Quaternion.Euler(rotationX, rotationY, 0);

        //the camera will follow the player and rotate around them
        transform.position = followTarget.position - targetRotation * new Vector3(0, cameraDistanceY, cameraDistanceZ);
        //camera will look at the player
        transform.rotation = targetRotation;
    }

    //only horizontal rotation, used for player move direction
    public Quaternion PlanarRotation => Quaternion.Euler(0, rotationY, 0);
}

