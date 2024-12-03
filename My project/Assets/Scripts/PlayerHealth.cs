using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;
    public int attack = 15;
    
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Player Health: " + health);
        if (health <= 0)
        {
            Debug.Log("Player Defeated!");
        }
    }
}
