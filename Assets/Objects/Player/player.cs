using System;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UIElements;

public class player : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    private float horisontalDirection = 0;
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
    }
    void Update()
    {
        MoveByXAxis();
        DoJump();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 normal = collision.GetContact(0).normal;
        if (normal.y > angleToJumpScale)
            isGrounded = true;
            print(normal.y);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
    void DoJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            rigidbody.linearVelocityY = jumpPower;
    }
    void MoveByXAxis()
    {
        horisontalDirection = Input.GetAxis("Horizontal");
        if (horisontalDirection != 0)
            rigidbody.linearVelocityX = Mathf.Lerp(rigidbody.linearVelocityX, horisontalDirection * speed, Time.deltaTime * acceleration);
        else if (Math.Abs(rigidbody.linearVelocityX) > 1)
            rigidbody.linearVelocityX = Mathf.Lerp(rigidbody.linearVelocityX, 0, Time.deltaTime * stopPower);
        else rigidbody.linearVelocityX = 0;







    }
}
