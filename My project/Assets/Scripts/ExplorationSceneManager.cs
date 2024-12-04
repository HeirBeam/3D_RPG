using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplorationSceneManager : MonoBehaviour
{
    public GameObject[] enemies; // Array of all enemies in the 3D scene

    void Start()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager instance is null!");
            return;
        }

        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                string enemyID = enemy.GetComponent<EnemyID>()?.enemyID;

                if (enemyID == null)
                {
                    Debug.LogError($"Enemy {enemy.name} is missing an EnemyID component!");
                    continue;
                }

                // Check if the enemy is defeated
                if (GameManager.Instance.IsEnemyDefeated(enemyID))
                {
                    Debug.Log($"Destroying defeated enemy in 3D world: {enemy.name}");
                    Destroy(enemy);
                }
            }
        }
    }
}
