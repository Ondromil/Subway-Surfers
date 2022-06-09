using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

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
    private bool isChangingSide = false;
    private float x = 0;

    private Vector2 startPos;
    private Vector2 currentPos;
    private bool didAction = false;
    
    public enum Tracks {Left, Mid, Right}
    private void Start()
    {
        transform.position = Vector3.zero;
        animator = GetComponent<Animator>();
        PlayerDeath.isDead = false;
        CoinScript.coins = 0;
        ScoreScript.score = 0;
    }
    
    private void FixedUpdate()
    {
        Vector3 move = transform.forward * moveSpeed / Time.fixedDeltaTime;
        if (!PlayerDeath.isDead)                                                 // Character moving forward
        {
            controller.Move(move);  
        }
        
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);   // Checks if character is on ground
        
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y -= gravity * Time.fixedDeltaTime;
        controller.Move(velocity * Time.fixedDeltaTime);
    } 
        
    private void Update()
    {
        //Ovládání pomocí WASD
        
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
            if (!isChangingSide)
            {
                position--;
                x = -5;
                StartCoroutine(ChangeTrack());
                FindObjectOfType<AudioManager>().Play("SwipeMove");
                animator.Play("MaxSneakers-Left");
            }
        }

        if (Input.GetKeyDown(KeyCode.D) && position != 1 && canHop && !PlayerDeath.isDead)   // Hopping right
        {
            if (!isChangingSide)
            {
                position++;
                x = 5;
                StartCoroutine(ChangeTrack());
                FindObjectOfType<AudioManager>().Play("SwipeMove");
                animator.Play("MaxSneakers-Right");
            }
        }
        
        //Ovládání pomocí myší  NENÍ HOTOVÝ
        
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
            Debug.Log(startPos);
            didAction = false;
        }
        if (Input.GetMouseButton(0))
        {
            currentPos = Input.mousePosition;
        }
        if ((startPos.y - currentPos.y) < -100 && isGrounded && !PlayerDeath.isDead && !didAction)
        {
            velocity.y = jumpHeight;
            FindObjectOfType<AudioManager>().Play("SwipeUp");
            animator.Play("MaxSneakers-JumpA");
            didAction = true;
        }
        
    
        
        
        // Ošetření, pokud se hráč nedostane do správné pozice
        
        if (position == 0 && transform.position.x != 0 && !isChangingSide)
        {
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
        }
        
        if (position == -1 && transform.position.x != -4.8f && !isChangingSide)
        {
            transform.position = new Vector3(-4.8f, transform.position.y, transform.position.z);
        }
        
        if (position == 1 && transform.position.x != 4.9f && !isChangingSide)
        {
            transform.position = new Vector3(4.9f, transform.position.y, transform.position.z);
        }
    }
    private IEnumerator SlideCrouch()     // Slide crouching function
    {
        isSliding = true;
        controller.height = 0.5f;
        controller.center -= new Vector3(0, 0.8f,0);
        PlayerDeath.checkRadius = 0.5f;
        
        yield return new WaitForSeconds(0.6f);
        
        controller.height = 2.9f;
        controller.center += new Vector3(0, 0.8f,0);
        PlayerDeath.checkRadius = 1.5f;
        isSliding = false;
    }

    private IEnumerator ChangeTrack()     // Dynamický pohyb hráče do stran
    {
        isChangingSide = true;
        transform.DOMoveX(transform.position.x + x, 0.15f);
        yield return new WaitForSeconds(0.1f);
        hop = new Vector3(x, 0, 0);
        controller.Move(hop);
        yield return new WaitForSeconds(0.1f);
        isChangingSide = false;
    }
}


