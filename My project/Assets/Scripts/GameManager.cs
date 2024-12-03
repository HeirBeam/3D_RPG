using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool enemyDefeated = false; // Track if the enemy is defeated

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep GameManager across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
