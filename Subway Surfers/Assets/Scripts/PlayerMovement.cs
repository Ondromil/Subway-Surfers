using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class PlayerMovement: MonoBehaviour
{
    public CharacterController controller;

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
    
    private Vector3 hop = Vector3.zero;

    [Header("Audio")]
    public AudioSource jumpAudio;
    public AudioSource crouchAudio;
    public AudioSource hopAudio;
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
            jumpAudio.Play();
        }
        
        if (Input.GetKeyDown(KeyCode.S))   // Slide crouching
        {
            velocity.y = -40; 
            if (!isSliding)
            { 
                StartCoroutine(SlideCrouch());     
            }
            crouchAudio.Play();
        }
        
        if (Input.GetKeyDown(KeyCode.A) && position != -1)  // Hopping left
        {
            position--;
            hop = new Vector3(-5,0,0);
            ChangeTrack();
            hopAudio.Play();
        }

        if (Input.GetKeyDown(KeyCode.D) && position != 1)   // Hopping right
        {
            position++; 
            hop = new Vector3(5,0,0);
            ChangeTrack();
            hopAudio.Play();
        }
        
    }
    private void ChangeTrack()
    {
        controller.Move(hop);
    }
    
    private IEnumerator SlideCrouch()
    {
        isSliding = true;
        controller.height = 0.5f;
        controller.center -= new Vector3(0, 0.2f,0);
        
        yield return new WaitForSeconds(0.6f);
        
        controller.height = 3f;
        controller.center += new Vector3(0, 0.2f,0);
        isSliding = false;
    }
}


