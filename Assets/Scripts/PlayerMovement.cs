using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float WalkSpeed = 15f;
    private float RunSpeed = 22.5f;
    private float CrouchSpeed = 7.5f;

    public CapsuleCollider PlayerCollider;
    public Camera PlayerCamera;
    private float StandHeight = 2f;
    private float CrouchHeight = 1f;

    private float JumpForce = 10f; 
    private bool IsGrounded;

    private Rigidbody RB;
    private bool IsCrouching = false;

    void OnCollisionStay(Collision collision)
    {
        IsGrounded = true;
    }

    void OnCollisionExit(Collision collision)
    {
        IsGrounded = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Crouch Input
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


        // Camera Control
        PlayerCamera.transform.localPosition = new Vector3(0, (PlayerCollider.height/2) - 0.1f, 0);

        // Movement
        float MoveX = Input.GetAxis("Horizontal");
        float MoveZ = Input.GetAxis("Vertical");


        Vector3 Move = transform.right * MoveX + transform.forward * MoveZ;

        float MovementSpeed;
        if (IsCrouching)
        {
            MovementSpeed = CrouchSpeed;
        }
        else
        {
            if (Input.GetButton("Run"))
            {
                MovementSpeed = RunSpeed;
            }
            else
            {
                MovementSpeed = WalkSpeed;
            }
        }


        Vector3 Velocity = Move * MovementSpeed;
        Velocity.y = RB.velocity.y;
        RB.velocity = Velocity;

        // Jump
        if (Input.GetButtonDown("Jump") && IsGrounded)
        {
            RB.velocity = new Vector3(RB.velocity.x, 0, RB.velocity.z);
            RB.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);    
        }
    }
}
