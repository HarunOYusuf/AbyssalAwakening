using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int maxHealth = 100;
    Animator animator;
    private bool isDead = false;
    
    private PlayerController playerController;
    void Start()
    {
        health = maxHealth;
        animator = GetComponent<Animator>();
    }
    

    public void TakeDamage(int amount)
    {
        if (isDead) return;
       
        health -= amount;
        if (health <= 0)
        {
            health = 0;
            Death();
        }
    }

    private void Death()
    {
        isDead = true;
        animator.SetTrigger("Death");
        StartCoroutine(HandleDeathAnimation());
    }


    private IEnumerator HandleDeathAnimation()
    {
        AnimatorStateInfo animationState = animator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(animationState.length + animationState.normalizedTime);

        Destroy(gameObject);
    }
}
