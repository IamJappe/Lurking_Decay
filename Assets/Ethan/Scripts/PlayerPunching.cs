using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPunching : MonoBehaviour
{
    public Animator handAnim;
   
    void Update()
    {
        Punch();
    }

    void Punch()
    {
        if(Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {
            handAnim.SetTrigger("Punch");
        }
    }
}
