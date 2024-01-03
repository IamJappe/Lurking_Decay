using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public int sensitivity = 200;
    public Camera cam;

    float mouseX;
    float mouseY; 
    
  
    private void Start()
    {
        //Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    float xRot;
    private void Update()
    {
        Mathf.Clamp(mouseY, -10, 10);

        mouseX += Input.GetAxisRaw("Mouse X") * sensitivity * Time.deltaTime;
        mouseY = Input.GetAxisRaw("Mouse Y") * sensitivity * Time.deltaTime;

        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -90,90);
       // Debug.Log(xRot);
        transform.rotation = Quaternion.Euler(0f ,mouseX, 0f);
        cam.transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
    }
}
