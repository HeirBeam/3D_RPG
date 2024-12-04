using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyID : MonoBehaviour
{
    public string enemyID; // Unique ID for this enemy

    void Awake()
    {
        if (string.IsNullOrEmpty(enemyID))
        {
            enemyID = System.Guid.NewGuid().ToString(); // Assign a unique ID if none is provided
        }
    }
}
