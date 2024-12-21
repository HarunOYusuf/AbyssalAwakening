using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;
    private bool movingToB = true;
    public float threshold = 0.1f;
    public PlayerHealth playerHealth;
    public int damage = 5;
    
    private void Update()
    {
    
        
    }

    void Patrol()
    {
        Vector2 targetPosition = movingToB ? pointB.position : pointA.position;

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPosition) <= threshold)
        {
            transform.position = targetPosition;
            movingToB = !movingToB;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerHealth.TakeDamage(damage);
        }
    }
}