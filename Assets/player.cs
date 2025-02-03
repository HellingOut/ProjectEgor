using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;

public class player : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int speed = 10;
    public new Rigidbody2D rigidbody;

    private float direction = 0;
    
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        direction = Input.GetAxis("Horizontal");
        if (rigidbody != null && direction != 0)
        {
            rigidbody.linearVelocityX = direction * speed;
        }
    }
}
