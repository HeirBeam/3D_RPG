using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 100;
    public int attack = 10;

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Enemy Health: " + health);
        if (health <= 0)
        {
            Debug.Log("Enemy Defeated!");
        }
    }
}
