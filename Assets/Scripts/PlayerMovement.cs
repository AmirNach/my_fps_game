using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player's movment script
public class PlayerMovement : MonoBehaviour
{
    private const float WalkSpeed = 15f;
    private const float RunSpeed = 22.5f;
    private const float CrouchSpeed = 7.5f;

    public CapsuleCollider PlayerCollider;
    public Camera PlayerCamera;
    private const float StandHeight = 2f;
    private const float CrouchHeight = 1f;

    private const float JumpForce = 10f;
    private bool IsGrounded;

    private Rigidbody RB;
    private bool IsCrouching = false;

    void OnCollisionStay(Collision collision)
    {
        IsGrounded = true; // The player is touching the ground
    }

    void OnCollisionExit(Collision collision)
    {
        IsGrounded = false; // The player left the ground
    }

    void Start()
    {
        RB = GetComponent<Rigidbody>(); // Get Rigidbody reference
    }

    void Update()
    {
        // Handle crouch input
        if (Input.GetButtonDown("Crouch") && IsGrounded)
            IsCrouching = true;

        if (Input.GetButtonUp("Crouch"))
            IsCrouching = false;

        if (IsCrouching)
        {
            PlayerCollider.height = CrouchHeight;
            PlayerCollider.center = new Vector3(0, (CrouchHeight / -2), 0);
        }
        else
        {
            PlayerCollider.height = StandHeight;
            PlayerCollider.center = new Vector3(0, 0, 0);
        }

        // Update camera position based on collider height
        PlayerCamera.transform.localPosition = new Vector3(0, (PlayerCollider.height / 2) - 0.1f, 0);

        // Movement input
        float MoveX = Input.GetAxis("Horizontal");
        float MoveZ = Input.GetAxis("Vertical");

        Vector3 Move = transform.right * MoveX + transform.forward * MoveZ;

        float MovementSpeed;
        if (IsCrouching)
        {
            MovementSpeed = CrouchSpeed; // Slower when crouching
        }
        else
        {
            MovementSpeed = Input.GetButton("Run") ? RunSpeed : WalkSpeed;
        }

        Vector3 Velocity = Move * MovementSpeed;
        Velocity.y = RB.velocity.y; // Keep vertical velocity
        RB.velocity = Velocity;

        // Jump input
        if (Input.GetButtonDown("Jump") && IsGrounded)
        {
            RB.velocity = new Vector3(RB.velocity.x, 0, RB.velocity.z);
            RB.AddForce(Vector3.up * JumpForce, ForceMode.Impulse); // Apply jump force
        }
    }
}
