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

    [Header("Crouch")]
    public float crouchHeight;
    public float originalHeight;
    bool isCrouching = false;
    public GameObject crouchPostProcessing;
    public float smoothTime = 0.2f; // Adjust the smooth time as needed
    public float crouchSpeed = 6f; // Adjust the crouch speed as needed
    public float standingSpeed = 10f;   

    private void Start()  
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()     
    {
        Movement();
        Crouch();
    }

    void Movement()
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

    void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            StartCoroutine(CrouchTransition());
        }
    }

    IEnumerator CrouchTransition()
    {
        isCrouching = !isCrouching;

        float targetHeight = isCrouching ? crouchHeight : originalHeight;

        float elapsedTime = 0f;
        float startHeight = controller.height;

        while (elapsedTime < smoothTime)
        {
            controller.height = Mathf.Lerp(startHeight, targetHeight, elapsedTime / smoothTime);
            elapsedTime += Time.deltaTime;

            speed = (int)Mathf.Lerp(speed, isCrouching ? crouchSpeed : standingSpeed, elapsedTime / smoothTime);

            yield return null;
        }

        controller.height = targetHeight;

        speed = isCrouching ? (int)crouchSpeed : (int)standingSpeed;

        crouchPostProcessing.SetActive(isCrouching);
    }

}
