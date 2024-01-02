using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float changeDirectionInterval = 2f;
    public float radius = 1f;
    public float castDistance = 5f;
    public Animator anim;

    bool shouldMove = true;

    private void Start()
    {
        InvokeRepeating("ChangeDirection", 0f, changeDirectionInterval);
    }

    private void Update()
    {
        Move();
        DetectPlayer();
    }

    void DetectPlayer()
    {
       Vector3 castOrigin = transform.position + transform.forward * radius;

        if (Physics.SphereCast(castOrigin, radius, transform.forward, out RaycastHit hit, castDistance))
        {
            Debug.DrawLine(castOrigin, hit.point, Color.red);

            if (hit.collider.CompareTag("Player"))
            {
                shouldMove = false;
                anim.SetBool("Idle", true);
                anim.SetBool("Run", false);
                Debug.Log("Player hit");
            }
        }
        else
        {
            shouldMove = true;
            anim.SetBool("Idle", false);
            anim.SetBool("Run", true);
            Debug.DrawRay(castOrigin, transform.forward * castDistance, Color.green);
        }
    }

    private void ChangeDirection()
    {
        if(shouldMove == true)
        {
            Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;

            Vector3 localRandomDirection = transform.TransformDirection(randomDirection);

            transform.forward = localRandomDirection;
        }
    }

    void Move()
    {
        if(shouldMove == true)
            transform.Translate(transform.forward * moveSpeed * Time.deltaTime);
    }
    
}
