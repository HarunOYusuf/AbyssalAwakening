using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
   
    public Transform FirePoint;
    public GameObject BulletPrefab;
    internal static string attack = "attack";
    public float walkSpeed = 3f;
    private Vector2 moveInput;
    
    private bool _isMoving = false;
    
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

    // Shooting Function
    void Shoot()
    {
        Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
    }
    
    //Player Movement 
    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * walkSpeed, rb.velocity.y);
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
        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
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
