using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class PlayerMovement : MonoBehaviour 
{
    int speed = 10;
    float horizontal;
    float vertical; 
    CharacterController controller;

    private void Start() 
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()    
    {
        horizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        vertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        Vector3 move = transform.forward * vertical + transform.right * horizontal;
        controller.Move(move);
    }
}
