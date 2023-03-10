using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;

    [Header("Gravity")]
    [SerializeField] private float gravity_raw;
    [SerializeField] private float gravity_multiplier;
    private float gravity;

    private Vector3 velocity;
    private float velocityY;


    [Header("Player Attributes")]
    [SerializeField] private float movement_speed;
    [SerializeField] private float foot_radius;
    [SerializeField] private float jump_height;
    [SerializeField] private Transform foot;
    [SerializeField] private LayerMask ground;
    private bool isGrounded;
    //private bool isJumping;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        gravity = gravity_raw * gravity_multiplier;
    }
    private void Update()
    {
        isGrounded = IsPlayerGrounded();
        //isJumping = false;

        Movement();
        Gravity();
        Jump();
        controller.Move(velocity * Time.deltaTime);
    }
    private bool IsPlayerGrounded()
    {
        return Physics.CheckSphere(foot.position, foot_radius, ground);
    }
    private void Movement()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        velocity  = (transform.right * inputX + transform.forward * inputY) * movement_speed;
    }
    private void Gravity()
    {   
        if (isGrounded == false)
        {
            velocityY += gravity * Time.deltaTime;
            velocity.y = velocityY;
        }
        else if (isGrounded == true && velocityY < 0.0f)
        {
            velocityY = 0.0f;
        }
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocityY = Mathf.Sqrt(2 * -gravity * jump_height);
            velocity.y = velocityY;
            //isJumping = true;
        }
    }
    // to be done
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {   
        // add roof layer and condition that player collides with object with roof layer
        //velocityY = 0.0f;
        //velocity.y= velocityY;
    }
}
