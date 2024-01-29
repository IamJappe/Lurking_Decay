using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StashWeapons : MonoBehaviour
{
    public KeyCode holdButton = KeyCode.E;
    public float holdTime = 1f;
    private bool isHolding = false;
    private float holdTimer = 0.0f;
    public GameObject hands;
    public Animator anim;

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
                    anim.SetTrigger("Stash");
                    StartCoroutine(Stash());
                    isHolding = false;
                }
            }
            else
            {
                isHolding = false;
            }
        }
    }

    IEnumerator Stash()
    {
        print("start");
        yield return new WaitForSeconds(3.5f);
        hands.SetActive(false);
        print("end");
    }
}
