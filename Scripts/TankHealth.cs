using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TankHealth : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;
    
    [SerializeField] int actualHealth;

    [SerializeField] GameObject explosion;

    int currentHealth;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        SetHealth();
    }

    public void TakeDamage()
    {
        currentHealth--;
        
        if (currentHealth <= 0)
        { 
            rb.velocity = Vector2.zero;
            animator.SetTrigger("murdered");
            Death();
        }
    }

    public void SetHealth()
    {
        currentHealth = actualHealth;
    }

    public void SetInvincible()
    {
        currentHealth = 100000000;
    }

    public void Death()
    {
        GamePlayManger GPM = GameObject.Find("Canvas").GetComponent<GamePlayManger>();
        
        if (gameObject.CompareTag("Player"))
        {
            GPM.SpawnPlayer();
        }
        
        else if (gameObject.CompareTag("Small"))
        {
            Tracker.smallTankDestroyed++;
            GPM.SetReserve();
            
        }
        else if (gameObject.CompareTag("Fast"))
        {
            Tracker.fastTankDestroyed++;
            GPM.SetReserve();
        }
        else if (gameObject.CompareTag("Heavy"))
        {
            Tracker.bigTankDestroyed++;
            GPM.SetReserve();
        }
        else if (gameObject.CompareTag("Armored"))
        {
            Tracker.armoredTankDestroyed++;
            GPM.SetReserve();
        }

        if (null != gameObject.GetComponent<BonusTank>() && gameObject.GetComponent<BonusTank>().IsBonusTankCheck())
        {
            GPM.GenerateBonusCrate();
        }
        
        Destroy(gameObject);

    }
}