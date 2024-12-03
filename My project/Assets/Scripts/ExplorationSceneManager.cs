using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplorationSceneManager : MonoBehaviour
{
    public GameObject enemy; // Reference to the enemy GameObject in the 3D scene

    void Start()
    {
        // Check if the enemy was defeated
        if (GameManager.Instance != null && GameManager.Instance.enemyDefeated)
        {
            if (enemy != null)
            {
                Destroy(enemy); // Destroy the enemy in the 3D scene
            }
        }
    }
}
