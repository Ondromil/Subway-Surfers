using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

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
    private bool canHop = true;
    private Vector3 hop = Vector3.zero;
    public static Animator animator;
    private float timeElapsed;
    
    private void Start()
    {
        transform.position = Vector3.zero;
        animator = GetComponent<Animator>();
        PlayerDeath.isDead = false;
    }
    
    private void FixedUpdate()
    {
        Vector3 move = transform.forward * moveSpeed / Time.fixedDeltaTime;
        if (!PlayerDeath.isDead)                                                 // Character moving forward
        {
            controller.Move(move);  
        }
        
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
        if (Input.GetKeyDown(KeyCode.W) && isGrounded && !PlayerDeath.isDead)    // Jumping
        {
            velocity.y = jumpHeight;
            FindObjectOfType<AudioManager>().Play("SwipeUp");
            animator.Play("MaxSneakers-JumpA");
        }
        
        if (Input.GetKeyDown(KeyCode.S) && !PlayerDeath.isDead)   // Slide crouching
        {
            velocity.y = -40; 
            if (!isSliding)
            { 
                StartCoroutine(SlideCrouch());     
            }
            FindObjectOfType<AudioManager>().Play("SwipeDown");
            animator.Play("MaxSneakers-Scroll");
        }
        
        if (Input.GetKeyDown(KeyCode.A) && position != -1 && canHop && !PlayerDeath.isDead)  // Hopping left
        {
            position--;
            hop = new Vector3(-4.8f, 0, 0);
            StartCoroutine(Lerp());
            FindObjectOfType<AudioManager>().Play("SwipeMove");
            animator.Play("MaxSneakers-Left");
      
        }

        if (Input.GetKeyDown(KeyCode.D) && position != 1 && canHop && !PlayerDeath.isDead)   // Hopping right
        {
            position++;
            hop = new Vector3(4.9f,0,0);
            StartCoroutine(Lerp());
            FindObjectOfType<AudioManager>().Play("SwipeMove");
            animator.Play("MaxSneakers-Right");
       
        }

        if (position == 0 && transform.position.x != 0)
        {
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
        }
        
        if (position == -1 && transform.position.x != -4.8f)
        {
            transform.position = new Vector3(-4.8f, transform.position.y, transform.position.z);
        }
        
        if (position == 1 && transform.position.x != 4.9f)
        {
            transform.position = new Vector3(4.9f, transform.position.y, transform.position.z);
        }
        
    }
    private IEnumerator SlideCrouch()     // Slide crouching function
    {
        isSliding = true;
        controller.height = 0.5f;
        controller.center -= new Vector3(0, 0.9f,0);
        PlayerDeath.checkRadius = 0.5f;
        
        yield return new WaitForSeconds(0.6f);
        
        controller.height = 2.9f;
        controller.center += new Vector3(0, 0.9f,0);
        PlayerDeath.checkRadius = 1.5f;
        isSliding = false;
    }

    private IEnumerator Lerp()     // Function, that changes track dynamically
    {
        canHop = false;
        float time = 0;
        float duration = 0.07f;
        Vector3 startPosition = transform.position;
        Vector3 endPosition = transform.position + hop;
        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        controller.Move(hop);
        canHop = true;
    }
}


