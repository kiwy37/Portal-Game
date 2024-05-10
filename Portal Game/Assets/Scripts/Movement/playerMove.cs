using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 6f;
    public float gravity = -9.81f;
    public Transform groundCheck;
    public float groundDistance = 2.2f;
    public LayerMask groundMask;
    public float jumpHeight = 3f;
    private float jumpMultiplier = 1f;
    Vector3 velocity;
    bool isGrounded;




    public void GainVelocity(float extra = -2)
    {
        
            velocity.y = Mathf.Sqrt(jumpHeight * extra * gravity * jumpMultiplier);
            jumpMultiplier = 1f;
        
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        Debug.Log(velocity.y);
        if (isGrounded && velocity.y < 0)
        {
            speed = 6f;
            velocity.y = -2f;

        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");



        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if ((Input.GetButtonDown("Jump")) && isGrounded)
        {
            GainVelocity();
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}