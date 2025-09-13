using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Control the plyer's look 
public class PlayerLook : MonoBehaviour
{
    private float MouseSensitivity = 1000f;
    public Transform PlayerBody;
    public Camera PlayerCamera;

    private float XRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock cursor to center
    }

    void Update()
    {
        // Get mouse input
        float MouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        float MouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;

        // Handle vertical rotation
        XRotation -= MouseY;
        XRotation = Mathf.Clamp(XRotation, -90f, 90f); // Clamp rotation to prevent over-rotation
        PlayerCamera.transform.localRotation = Quaternion.Euler(XRotation, 0f, 0f);

        // Handle horizontal rotation
        PlayerBody.Rotate(Vector3.up * MouseX);
    }
}
