using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class PlayerMovement : MonoBehaviour 
{
    int speed = 10;
    float horizontal;
    float vertical; 

    public GameObject backPackPanel;
    bool isOpen = false;
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

        //Open and close inventory 
        if(Input.GetKeyDown(KeyCode.I) && isOpen == false)
        {
            isOpen = true;
            backPackPanel.SetActive(true);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if(Input.GetKeyDown(KeyCode.I) && isOpen == true)
        {
            isOpen = false;
            backPackPanel.SetActive(false);
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }


    }
}
