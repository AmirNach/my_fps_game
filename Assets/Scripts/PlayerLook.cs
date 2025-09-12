using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    private float MouseSensitivity = 1000f;
    public Transform PlayerBody;  
    public Camera PlayerCamera;   

    private float XRotation = 0f; 

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; 
    }

    void Update()
    {
        // Mouse Input
        float MouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        float MouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;

        // Vertical Rotation
        XRotation -= MouseY;
        XRotation = Mathf.Clamp(XRotation, -90f, 90f);
        PlayerCamera.transform.localRotation = Quaternion.Euler(XRotation, 0f, 0f);

        // Horizontal Rotation
        PlayerBody.Rotate(Vector3.up * MouseX);
    }
}
