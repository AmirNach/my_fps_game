using UnityEngine;
using UnityEngine.UI;

// Minimap script
public class Minimap : MonoBehaviour
{
    [Header("Minimap Cameras")]
    public Camera SmallMinimapCamera;
    public Camera LargeMinimapCamera;

    [Header("Minimap UI")]
    public RawImage SmallMinimapUI;
    public RawImage LargeMinimapUI;

    [Header("Player UI")]
    public GameObject PlayerStatsUI;

    [Header("Player")]
    public Transform Player;
    public PlayerMovement PlayerMovementScript; // Reference to the player's movement

    [Header("Weapon")]
    public Weapon PlayerWeaponScript; // Reference to the weapon

    [Header("Settings")]
    public float SmallHeight = 50f;
    public float LargeHeight = 100f;
    public int LargeResolution = 1024;

    [Header("Large Map Controls")]
    public bool LargeMapActive = false;
    public float DragSpeed = 0.5f; // Drag speed
    public float ZoomSpeed = 10f;  // Zoom speed
    public float MinZoom = 50f;
    public float MaxZoom = 200f;

    private Vector3 dragOffset;
    private Vector3 lastMousePos;
    private bool dragging = false;

    void Start()
    {
        // Small map render texture
        RenderTexture smallRT = new RenderTexture(256, 256, 16);
        SmallMinimapCamera.targetTexture = smallRT;
        SmallMinimapUI.texture = smallRT;

        // Large map render texture
        RenderTexture largeRT = new RenderTexture(LargeResolution, LargeResolution, 16);
        largeRT.antiAliasing = 4;
        LargeMinimapCamera.targetTexture = largeRT;
        LargeMinimapUI.texture = largeRT;

        LargeMinimapUI.gameObject.SetActive(false); // start hidden
    }

    void LateUpdate()
    {
        if (Player == null) return;

        // Small camera always above player
        Vector3 smallPos = Player.position;
        smallPos.y = SmallHeight;
        SmallMinimapCamera.transform.position = smallPos;
        SmallMinimapCamera.transform.rotation = Quaternion.Euler(90f, 0f, 0f);

        // Handle drag and zoom only if large map active
        if (LargeMapActive)
        {
            HandleDrag(); // Drag camera
            HandleZoom(); // Zoom camera
        }

        // Toggle map key
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleLargeMap();
        }
    }

    void ToggleLargeMap()
    {
        LargeMapActive = !LargeMapActive;
        LargeMinimapUI.gameObject.SetActive(LargeMapActive);

        if (LargeMapActive)
        {
            Cursor.lockState = CursorLockMode.None; // Unlock the cursor
            Cursor.visible = true;

            if (PlayerStatsUI != null)
                PlayerStatsUI.SetActive(false); // Hide player UI

            if (PlayerMovementScript != null)
                PlayerMovementScript.enabled = false; // Disable movement

            if (PlayerWeaponScript != null)
                PlayerWeaponScript.enabled = false; // Disable shooting

            // Set initial center for large map
            Vector3 centerPos = Player.position;
            centerPos.y = LargeHeight;
            LargeMinimapCamera.transform.position = centerPos;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked; // Lock the cursor
            Cursor.visible = false;

            if (PlayerStatsUI != null)
                PlayerStatsUI.SetActive(true); // Show player UI

            if (PlayerMovementScript != null)
                PlayerMovementScript.enabled = true; // Enable movement

            if (PlayerWeaponScript != null)
                PlayerWeaponScript.enabled = true; // Enable shooting
        }
    }

    void HandleDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePos = Input.mousePosition;
            dragging = true; // Start dragging
        }

        if (Input.GetMouseButtonUp(0))
            dragging = false; // Stop dragging

        if (dragging)
        {
            Vector3 delta = Input.mousePosition - lastMousePos;
            delta *= DragSpeed;
            LargeMinimapCamera.transform.position -= new Vector3(delta.x, 0f, delta.y); // Move camera
            lastMousePos = Input.mousePosition;
        }
    }

    void HandleZoom()
    {
        float scroll = Input.mouseScrollDelta.y;
        if (scroll != 0f)
        {
            float newSize = LargeMinimapCamera.orthographicSize - scroll * ZoomSpeed;
            LargeMinimapCamera.orthographicSize = Mathf.Clamp(newSize, MinZoom, MaxZoom); // Clamp zoom
        }
    }
}
