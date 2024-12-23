using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WorldLimits : MonoBehaviour

{
  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.CompareTag("Player"))
    {
      PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

      if (playerHealth != null)
      {
        
        playerHealth.Death();
      }
   
    }
  }
}
