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
    private bool stoppedBySurface = false;
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
        //rigidbody.Slide();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        normal = collision.GetContact(0).normal;
        if (normal.y > angleToJumpScale){
            isGrounded = true;            
            stoppedBySurface = false;
            rigidbody.sharedMaterial = friction;
            print("friction");
        }
        if(normal.y <= 0 && !isGrounded){
            rigidbody.linearVelocityY = 
                    Mathf.Lerp(rigidbody.linearVelocityY,
                        0, Time.deltaTime * rigidbody.gravityScale);
            rigidbody.sharedMaterial = noFriction;
            print("NoFriction");
        }
    }

    private void OnCollisionStay2D(Collision2D collision){
        normal = collision.GetContact(0).normal;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
            
        print("out");
    }

    void MoveByXAxis()
    {
        horisontalDirection = Input.GetAxis("Horizontal");
        if(horisontalDirection == 0){
           rigidbody.linearVelocityX = Mathf.Lerp(rigidbody.linearVelocityX,0,Time.deltaTime* stopPower);
        }
        // if (horisontalDirection < 0 && !stoppedBySurface)
        //     if(normal.x  < 0) {
        //         rigidbody.linearVelocityX = Mathf.Lerp(rigidbody.linearVelocityX, horisontalDirection * speed / (Mathf.Abs(normal.y) + 0.1F), Time.deltaTime * acceleration);
        //         rigidbody.linearVelocityY = Mathf.Lerp(rigidbody.linearVelocityY, speed * normal.x, Time.deltaTime * acceleration);
        //     }
        //     else{
        //     rigidbody.linearVelocityX = Mathf.Lerp(rigidbody.linearVelocityX, horisontalDirection * speed / (Mathf.Abs(normal.y) + 0.1F), Time.deltaTime * acceleration);
        //     rigidbody.linearVelocityY = Mathf.Lerp(rigidbody.linearVelocityY, speed * normal.x * -1, Time.deltaTime * acceleration);
        //     }
        if(horisontalDirection < 0 && isGrounded == false){ // Движение в лево при прыжке
            rigidbody.linearVelocityX = 
                Mathf.Lerp(rigidbody.linearVelocityX,
                    horisontalDirection * (speed / Mathf.Abs(normal.y)), Time.deltaTime * acceleration); 
        }
        else if(horisontalDirection > 0 && isGrounded == false){ // Движение в право при прыжке
            rigidbody.linearVelocityX = 
                Mathf.Lerp(rigidbody.linearVelocityX,
                    horisontalDirection * (speed / Mathf.Abs(normal.y)), Time.deltaTime * acceleration);
        }
       else if(horisontalDirection < 0 && isGrounded){ // движение в лево
            if(normal.x < 0){ // Движение  в лево при спуске
                rigidbody.linearVelocityX = 
                    Mathf.Lerp(rigidbody.linearVelocityX,
                         horisontalDirection * (speed / Mathf.Abs(normal.y)), Time.deltaTime * acceleration); // Жвижение в лево
                rigidbody.linearVelocityY = 
                    Mathf.Lerp(rigidbody.linearVelocityY,
                        speed * normal.x, Time.deltaTime * acceleration); // компенсация для устранения мини прыжков
            }
            else{ // движение в лево при подъеме
                rigidbody.linearVelocityX = 
                    Mathf.Lerp(rigidbody.linearVelocityX,
                        horisontalDirection * (speed / Mathf.Abs(normal.y)), Time.deltaTime * acceleration); 
            }
        }
        else if(horisontalDirection > 0 && isGrounded){ // движение в право
            if(normal.x > 0){ // движение в право при спуске
                rigidbody.linearVelocityX = 
                    Mathf.Lerp(rigidbody.linearVelocityX,
                        horisontalDirection * (speed / Mathf.Abs(normal.y)), Time.deltaTime * acceleration);
                rigidbody.linearVelocityY = 
                    Mathf.Lerp(rigidbody.linearVelocityY, 
                        speed * normal.x * -1, Time.deltaTime * acceleration); // компенсация для устранения мини прыжков
            }
            else{ // движение в право при подъеме
                rigidbody.linearVelocityX = 
                    Mathf.Lerp(rigidbody.linearVelocityX,
                        horisontalDirection * (speed / Mathf.Abs(normal.y)), Time.deltaTime * acceleration);
            }
        }  
        
        print(normal.y);
    }
    void MoveByYAxis(){
        verticalDirection = Input.GetAxis("Vertical");
        if(verticalDirection != 0){

        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded){
            rigidbody.linearVelocityY = jumpPower;
            rigidbody.sharedMaterial = noFriction;
            isGrounded = false;
        }
    }
}
