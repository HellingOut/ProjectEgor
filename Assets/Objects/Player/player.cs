using System;
using UnityEngine;

public class player : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    private Vector2 direction;
    private Vector2 collisionNormal;
    public int speed = 10;
    public float jumpPower = 5;
    public float topSpeed = 10;
    public float stopPower = 11;
    public float acceleration = 2;
    public float minFloorAngle = 0.5F;
    public float friction = 1.5F;
    public Vector2 velocity = Vector2.zero;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        MoveByXAxis();
        MoveByYAxis();
        MoveAndSlide();
    }

    void MoveAndSlide()
    {
        Rigidbody2D.SlideResults slideResults = rigidbody.Slide(velocity, Time.deltaTime, new Rigidbody2D.SlideMovement());
        collisionNormal = slideResults.surfaceHit.normal;
    }
    void MoveByXAxis()
    {
        direction.x = Input.GetAxis("Horizontal");
        if (direction.x != 0)
            velocity.x = Mathf.Lerp(velocity.x, direction.x * speed, Time.deltaTime * acceleration);
        else if (Math.Abs(velocity.x) > 1)
            velocity.x = Mathf.Lerp(velocity.x, 0, Time.deltaTime * stopPower);
        else velocity.x = 0;
    }

    bool IsGrounded()
    {
        if (collisionNormal.y > minFloorAngle)
            return true;
        else
            return false;
    }

    void MoveByYAxis(){
        /*if (!IsGrounded())
            velocity.y += Physics2D.gravity.y * Time.deltaTime;
        else
            velocity.y = 0;
        */
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
            velocity.y = jumpPower;
    }
}
