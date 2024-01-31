using System.Collections;
using System.Globalization;
using Unity.Netcode;
using UnityEngine;

public class PlayerPunching : NetworkBehaviour
{
    public Animator handAnim;
    public Transform punchPoint;
    public float punchRange = 2f;
    public GameObject bloodEffect;
    private bool isPunching = false;
    // public HealthSystem system;  // Assuming you have a HealthSystem component attached

    private void Start()
    {
        if (!IsOwner)
        {
            this.enabled = false;
        }
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isPunching)
        {
            StartCoroutine(Punch());
        }
    }

    IEnumerator Punch()
    {
        isPunching = true;

        handAnim.SetTrigger("Punch");

        yield return new WaitForSeconds(0.01f);

        RayCasts();

        yield return new WaitForSeconds(0.5f);

        handAnim.ResetTrigger("Punch");
        handAnim.SetTrigger("Bob");

        yield return new WaitForSeconds(0.1f);

        isPunching = false;
    }

    void RayCasts()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, punchRange))
        {
            if (hit.collider.CompareTag("Dummy"))
            {
                Debug.Log("Punched enemy");
                hit.collider.gameObject.SendMessage("Damage", 1);
                Instantiate(bloodEffect, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
            }
        }
    }
}
