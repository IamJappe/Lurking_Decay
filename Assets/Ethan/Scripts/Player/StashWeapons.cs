using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StashWeapons : MonoBehaviour
{
    public KeyCode holdButton = KeyCode.Space;
    public float holdTime = 2.0f;
    private bool isHolding = false;
    private float holdTimer = 0.0f;

    private void Update()
    {
        if (Input.GetKeyDown(holdButton))
        {
            isHolding = true;
            holdTimer = 0.0f;
        }

        if (isHolding)
        {
            holdTimer += Time.deltaTime;

            if (Input.GetKey(holdButton))
            {
                if (holdTimer >= holdTime)
                {
                    Debug.Log("Button held down for" + holdTime + "seconds");
                }
            }
            else
            {
                isHolding = false;
            }
        }
    }
}
