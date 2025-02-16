using System;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.U2D;
using UnityEngine.UIElements;

public class player : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    private new SpriteRenderer spriteRenderer;
    private Animator animator;
    public PhysicsMaterial2D friction;
    public PhysicsMaterial2D noFriction;
    private Vector2 normal = Vector2.zero;
    private float horisontalDirection = 0;
    private float verticalDirection = 0;
    private bool isGrounded = false;
    public float gravityScale = 1;
    public float speed = 3;
    public float sprint = 2;
    public float jumpPower = 5;
    public float topSpeed = 15;
    public float stopPower = 11;
    public float acceleration = 15;
    public float angleToJumpScale = 0.5F;
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody.sharedMaterial = friction;        
    }
    void Update(){
        MoveByXAxis();
        MoveByYAxis();
    }
   // void Update()
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        normal = collision.GetContact(0).normal;
        if (normal.y > angleToJumpScale){
            isGrounded = true;   
            animator.SetBool("Grounded",isGrounded);         
        }
        if(normal.y <= 0 && !isGrounded){
            rigidbody.linearVelocityY = 
                    Mathf.Lerp(rigidbody.linearVelocityY,
                        0, Time.deltaTime * rigidbody.gravityScale);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {  
            if(isGrounded)
                if(collision.gameObject.tag == "Surface")
                    isGrounded = false;
            animator.SetBool("Grounded",isGrounded);    
        
        
    }
private void OnCollisionStay2D(Collision2D collision)
{
    if(collision.gameObject.tag == "Surface"){
         normal = collision.GetContact(0).normal;
        if (normal.y > angleToJumpScale){
            isGrounded = true;   
            animator.SetBool("Grounded",isGrounded);         
        }
    }
}

    void MoveByXAxis()
    {
        horisontalDirection = Input.GetAxis("Horizontal");
        if(horisontalDirection > 0)
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else if (horisontalDirection < 0){
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1 , transform.localScale.y, transform.localScale.z);
        }
        if(horisontalDirection == 0){
            EnableFriction();
           rigidbody.linearVelocityX = Mathf.Lerp(rigidbody.linearVelocityX,0,Time.deltaTime* stopPower);
            animator.SetFloat("Walking",0);
        }
        if(horisontalDirection != 0 && Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)){
            DisableFriction();
            rigidbody.linearVelocityX = Mathf.Lerp(rigidbody.linearVelocityX,
                horisontalDirection * (speed + sprint), Time.deltaTime * acceleration);
                animator.SetFloat("Walking", Mathf.Abs(horisontalDirection));
        }
        if(horisontalDirection != 0){
            DisableFriction();
            rigidbody.linearVelocityX = Mathf.Lerp(rigidbody.linearVelocityX,
                horisontalDirection * speed, Time.deltaTime * acceleration);
                animator.SetFloat("Walking", Mathf.Abs(horisontalDirection));
        }
        if (rigidbody.linearVelocityX > topSpeed){
            rigidbody.linearVelocityX = topSpeed;
        }
        
    }
    void MoveByYAxis(){
        verticalDirection = Input.GetAxis("Vertical");
        if(verticalDirection != 0){
            EnableFriction();
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded){
            DisableFriction();
            rigidbody.linearVelocityY = jumpPower;
            rigidbody.sharedMaterial = noFriction;
            isGrounded = false;
            animator.SetBool("Grounded",isGrounded);
        }
        animator.SetFloat("VelocityY",rigidbody.linearVelocityY);
    }
    void EnableFriction(){
        rigidbody.sharedMaterial = friction;
    }
    void DisableFriction(){
        rigidbody.sharedMaterial = noFriction;
    }
}
