using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float changeDirectionInterval = 2f;
    public float radius = 1f;
    public float castDistance = 5f;
    public GameObject rayPoint;

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
        if (Physics.SphereCast(rayPoint.transform.position, radius, transform.forward, out RaycastHit hit, castDistance))
        {
            if (hit.collider.CompareTag("Player"))
            {
                shouldMove = false;
                Debug.Log("Player is in radius");
            }
            else
            {
                shouldMove = true;
            }
        }
        else
        {
            // Draw a debug line to visualize the cast's maximum distance
            Debug.DrawRay(transform.position, transform.forward * castDistance, Color.green);
        }
    }

    private void ChangeDirection()
    {
        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;

        Vector3 localRandomDirection = transform.TransformDirection(randomDirection);

        transform.forward = localRandomDirection;
    }

    void Move()
    {
        if(shouldMove == true)
            transform.Translate(transform.forward * moveSpeed * Time.deltaTime);
    }
    
}
