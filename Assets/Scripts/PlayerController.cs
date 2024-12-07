using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    
    internal static string attack = "attack";
    public float walkSpeed = 3f;
    public float jumpForce = 2f;
    private Vector2 moveInput;
    public Vector2 flipOffset = new Vector2(0.5f, 0f);
    
    private bool _isMoving = false;
    private bool _isGrounded = false;
    
    //Player movement anim
    public bool IsMoving
    {
        get { return _isMoving; }
        private set
        {
            _isMoving = value;
            animator.SetBool("isMoving", value);
        }
    }
    
    // Flip Player direction
    
    public bool _isFacingRight = true;

    public bool IsFacingRight { get { return _isFacingRight; } private set {
            if (_isFacingRight != value)
            {
               transform.Rotate(0f, 180f, 0f);
            }
            _isFacingRight = value;
        }
    }

    Rigidbody2D rb;
    private Animator animator;
    
    public LayerMask groundLayer; 
    public Transform groundCheck; 
    public float groundCheckRadius = 0.2f;
    
    //Anim setup
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); 
    }
    
  
    void Start()
    {

    }

    // 
    void Update()
    {
     
        
    }
    
    
    //Player Movement 
    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * walkSpeed, rb.velocity.y);
        
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        animator.SetBool("isGrounded", _isGrounded);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        
        IsMoving = moveInput != Vector2.zero;
        
        SetFacingDirection(moveInput);
    }

    
    
    
    private void SetFacingDirection(Vector2 vector2)
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
            AdjustFlipOffset();
        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
            AdjustFlipOffset();
        }
        
    }
    
    //Adjusting Pivot
    private void AdjustFlipOffset()
    {
        
        Vector2 position = transform.position;
        position.x += IsFacingRight ? flipOffset.x : -flipOffset.x;
        transform.position = position;
    }
    
    //Jump Function
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && _isGrounded)
        {
          
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetTrigger("jump"); 
        }
    }

    // Player Attack
    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.started)
        {
          
            animator.SetTrigger(attack);
            
            moveInput = Vector2.zero;
            IsMoving = false;
            
         
        }
    }

}
