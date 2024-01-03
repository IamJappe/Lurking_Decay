using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBob : MonoBehaviour
{
    public Animator anim;
    public bool walking;
    void Update()
    {
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            anim.ResetTrigger("Idle");
            anim.ResetTrigger("Run");
            anim.SetTrigger("Walk");
            walking = true;
            if(walking)
            {
                if(Input.GetKey(KeyCode.LeftShift))
                {
                    anim.ResetTrigger("Walk");
                    anim.ResetTrigger("Idle");
                    anim.SetTrigger("Run");
                }
            }
             
        }
        else
        {
            walking = false;
            anim.ResetTrigger("Run");
            anim.ResetTrigger("Walk");
            anim.SetTrigger("Idle");
        }
    }
}
