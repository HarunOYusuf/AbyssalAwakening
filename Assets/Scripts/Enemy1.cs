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
}