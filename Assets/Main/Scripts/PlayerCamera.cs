using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public int sensitivity = 200;

    float mouseX;
    float mouseY; 
    
  
    private void Start()
    {
        //Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        mouseX += Input.GetAxisRaw("Mouse X") * sensitivity * Time.deltaTime;
        mouseY -= Input.GetAxisRaw("Mouse Y") * sensitivity * Time.deltaTime;

        transform.rotation = Quaternion.Euler(0f ,mouseX, 0f);
        Camera.main.transform.localRotation = Quaternion.Euler(mouseY, 0f, 0f);
    }
}
