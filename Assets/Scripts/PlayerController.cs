using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    internal static string roll = "roll";
    internal static string jump = "jump";
    internal static string attack = "attack";
    public float rollDuration = 0.8f;
    public float rollSpeedMultiplier = 2f;
    public float rollForce = 5f;
    public float walkSpeed = 3f;
    public float jumpForce = 2f;
    public  float attackDuration = 0.5f;
    private Vector2 moveInput;
    public Vector2 flipOffset = new Vector2(0.5f, 0f);
    
    private bool _isMoving = false;
    private bool _isGrounded = false;
    private bool _isAttacking = false;
    public bool _isRolling = false;
    
    
    //Player movement anim setup
    public bool IsMoving
    {
        get { return _isMoving; }
        private set
        {
            _isMoving = value;
            animator.SetBool("isMoving", value);
        }
    }
    
    //Player Attack anim setup
    public bool IsAttacking
    {
        get { return _isAttacking; }
    }
    
    //Roll anim setup
    public bool IsRolling
    {
        get { return _isRolling; }
        private set
        {
            _isRolling = value;
            animator.SetBool("isRolling", value);
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
    
    //Ground Checks
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
        if (_isAttacking || _isRolling)
            return;
        
        rb.velocity = new Vector2(moveInput.x * walkSpeed, rb.velocity.y);
             
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        animator.SetBool("isGrounded", _isGrounded);
        animator.SetBool("isMoving", _isMoving);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (_isAttacking || _isRolling)
            return;
        
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
        if(context.started && !_isAttacking)
        {
            _isAttacking = true;
                animator.Play("Player2Attack1");
            
            animator.SetBool("isAttacking", true);
            
            rb.velocity= Vector2.zero;
           
            StartCoroutine(ResetAttackFlag());

        }
    }

    //Attack Ccroutine
    private IEnumerator ResetAttackFlag()
    { 
        yield return new WaitForSeconds(attackDuration);
        
        _isAttacking = false;
        animator.SetBool("isAttacking", false);
    }
    
    //Rolling
    public void OnRoll(InputAction.CallbackContext context)
    {
        if (context.started && !_isRolling && !_isAttacking && _isGrounded) 
        {
            StartCoroutine(PerformRoll());
        }
    }
    
    // Roll Coroutine
    private IEnumerator PerformRoll()
    {
        IsRolling = true; 
        animator.SetTrigger(roll); 
        _isRolling = true;

        float rollDirection = IsFacingRight ? 1f : -1f;
        rb.velocity = new Vector2(rollDirection * rollForce, rb.velocity.y);
        
        yield return new WaitForSeconds(rollDuration); 
        
        _isRolling = false;
        IsRolling = false; 
    }
}

