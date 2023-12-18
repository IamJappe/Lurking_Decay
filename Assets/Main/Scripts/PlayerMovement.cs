using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class PlayerMovement : MonoBehaviour 
{
    [Header("Settings")]
    public float gravity = -9.81f;
    public int jumpPower = 5;
    public int speed = 10;
    [Header("Gravity")]
    public Transform groundCheck;
    public LayerMask mask;
    bool isGrounded;

    float horizontal;
    float vertical; 
    CharacterController controller;
    Vector3 velocity;

    private void Start()  
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()    
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, .5f, mask);
        horizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        vertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        Vector3 move = transform.forward * vertical + transform.right * horizontal;
        controller.Move(move);
        
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = jumpPower;
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
