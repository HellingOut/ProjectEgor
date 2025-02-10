using System;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UIElements;

public class player : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    public PhysicsMaterial2D friction;
    public PhysicsMaterial2D noFriction;
    private Vector2 normal = Vector2.zero;
    private float horisontalDirection = 0;
    private float verticalDirection = 0;
    private bool isGrounded = false;
    public int speed = 10;
    public float jumpPower = 5;
    public float topSpeed = 10;
    public float stopPower = 11;
    public float acceleration = 2;
    public float angleToJumpScale = 0.5F;
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.sharedMaterial = friction;
    }
    void Update()
    {
        MoveByXAxis();
        MoveByYAxis();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        normal = collision.GetContact(0).normal;
        if (normal.y > angleToJumpScale){
            isGrounded = true;            
        }
        if(normal.y <= 0 && !isGrounded){
            rigidbody.linearVelocityY = 
                    Mathf.Lerp(rigidbody.linearVelocityY,
                        0, Time.deltaTime * rigidbody.gravityScale);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }

    void MoveByXAxis()
    {
        horisontalDirection = Input.GetAxis("Horizontal");
        print(horisontalDirection);
        if(horisontalDirection == 0){
            EnableFriction();
           rigidbody.linearVelocityX = Mathf.Lerp(rigidbody.linearVelocityX,0,Time.deltaTime* stopPower);
           
        }
        if(horisontalDirection != 0){
            DisableFriction();
            rigidbody.linearVelocityX = Mathf.Lerp(rigidbody.linearVelocityX,
                horisontalDirection * speed, Time.deltaTime * acceleration);
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
        }
    }
    void EnableFriction(){
        rigidbody.sharedMaterial = friction;
    }
    void DisableFriction(){
        rigidbody.sharedMaterial = noFriction;
    }
}
