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
    private Animator animator;
    private Animation animation;
    public PhysicsMaterial2D friction;
    public PhysicsMaterial2D noFriction;
    private Vector2 normal = Vector2.zero;
    private float horisontalDirection = 0;
    private float verticalDirection = 0;
    private bool isGrounded = false;
    public float gravityScale = 1;
    public int speed = 10;
    public float jumpPower = 5;
    public float topSpeed = 10;
    public float stopPower = 11;
    public float acceleration = 2;
    public float angleToJumpScale = 0.5F;
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rigidbody.sharedMaterial = friction;
        animation = GetComponent<Animation>();
        
        
        
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
            animator.SetBool("Grounded",true);         
        }
        if(normal.y <= 0 && !isGrounded){
            rigidbody.linearVelocityY = 
                    Mathf.Lerp(rigidbody.linearVelocityY,
                        0, Time.deltaTime * rigidbody.gravityScale);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {  
        if(collision.gameObject.tag == "Surface"){
            isGrounded = false;
            animator.SetFloat("Jumping",rigidbody.linearVelocityY);
        }
    }

    void MoveByXAxis()
    {
        horisontalDirection = Input.GetAxis("Horizontal");
        if(horisontalDirection == 0){
            EnableFriction();
           rigidbody.linearVelocityX = Mathf.Lerp(rigidbody.linearVelocityX,0,Time.deltaTime* stopPower);
            animator.SetFloat("Walking",0);
        }
        if(horisontalDirection != 0){
            DisableFriction();
            rigidbody.linearVelocityX = Mathf.Lerp(rigidbody.linearVelocityX,
                horisontalDirection * speed, Time.deltaTime * acceleration);
                animator.SetFloat("Walking",Mathf.Abs(horisontalDirection));
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
             animator.SetFloat("Jumping",rigidbody.linearVelocityY);
            isGrounded = false;
            animator.SetBool("Grounded",false);
        }
        else
            animator.SetFloat("Jumping",rigidbody.linearVelocityY);
    }
    void EnableFriction(){
        rigidbody.sharedMaterial = friction;
    }
    void DisableFriction(){
        rigidbody.sharedMaterial = noFriction;
    }
}
