using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement: MonoBehaviour
{
    public CharacterController controller;
    public GameObject player;

    [Header("Gravity Settings")]
    public float gravity = 9.81f;
    private Vector3 velocity;

    [Header("Ground Check Settings")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    [SerializeField]
    private bool isGrounded;

    [Header("Player behaviour")]
    public float moveSpeed = 5;
    public float jumpHeight = 3f;
    
    private int position = 0;
    private bool isSliding;
    void Start()
    {
        
    }
    
    private void FixedUpdate()
    {
        Vector3 move = transform.forward * moveSpeed / Time.fixedDeltaTime;    
        controller.Move(move);                                             // Character moving forward
        
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);    // Checks if character is on ground
        
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y -= gravity * Time.fixedDeltaTime;
        controller.Move(velocity * Time.fixedDeltaTime);
        
    } 
        
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)    // Jumping
        {
            velocity.y = jumpHeight;
        }
        
        if (Input.GetKeyDown(KeyCode.S))   // Slide crouching
        {
            velocity.y = -40; 
            if (!isSliding)
            { 
                StartCoroutine(SlideCrouch());     
            }
        }
        
        if (Input.GetKeyDown(KeyCode.A) && position != -1)  // Hopping left
        {
            position--;
            controller.center -= new Vector3(1.5f,0,0);
            transform.position -= new Vector3(4, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.D) && position != 1)   // Hopping right
        {
            position++;
            controller.center += new Vector3(1.5f,0,0);
            transform.position += new Vector3(4, 0, 0);
        } 
    
       

    }

    private IEnumerator SlideCrouch()
    {
        isSliding = true;
        controller.height = 0.5f;
        controller.center -= new Vector3(0, 0.2f,0);
        
        yield return new WaitForSeconds(0.6f);
        
        controller.height = 1f;
        controller.center += new Vector3(0, 0.2f,0);
        isSliding = false;
    }
}


