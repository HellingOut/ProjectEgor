using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UIElements;

public class player : MonoBehaviour
{
    public int speed = 10;
    private new Rigidbody2D rigidbody;
    private bool isGrounded = false;
    private float horisontalDirection = 0;
    public float jumpPower = 5;
    public float TopSpeed = 10;
    public float stopPower = 12;
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
        if (normal == Vector2.up)
            isGrounded = true;
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
        if (rigidbody is not null)
            if (horisontalDirection != 0)
                if (Mathf.Abs(rigidbody.linearVelocityX) < TopSpeed)
                    rigidbody.AddForce(new Vector2(horisontalDirection * speed, 0));
                else if (Mathf.Abs(rigidbody.linearVelocityX) > 0.1)
                    rigidbody.linearVelocityX =
                        Mathf.Lerp(rigidbody.linearVelocityX, 0, Time.deltaTime * stopPower);
                else
                    rigidbody.linearVelocityX = 0;
    }
}
