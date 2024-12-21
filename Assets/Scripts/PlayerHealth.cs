using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Image healthBar;
    public int health;
    public int maxHealth = 100;
    Animator animator;
    private bool isDead = false;
    
    private PlayerController playerController;
    void Start()
    {
        health = maxHealth;
        animator = GetComponent<Animator>();
        UpdateHealthBar();
    }



    public void TakeDamage(int amount)
    {
        if (isDead) return;
       
        health -= amount;
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthBar();

        animator.SetTrigger("Hurt");
        if (health <= 0)
        {
            health = 0;
            Death();
        }
    }
    
    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = (float)health / maxHealth;
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
