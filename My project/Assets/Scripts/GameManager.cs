using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject enemyPrefab; // Cloned prefab for combat
    public GameObject originalEnemy; // Original enemy in the 3D world
    public string currentEnemyName;

    


    public Dictionary<string, bool> defeatedEnemies = new Dictionary<string, bool>();

    void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void MarkEnemyAsDefeated(string enemyID)
    {
        if (!defeatedEnemies.ContainsKey(enemyID))
        {
            defeatedEnemies.Add(enemyID, true);
        }
    }

    public bool IsEnemyDefeated(string enemyID)
    {
        return defeatedEnemies.ContainsKey(enemyID) && defeatedEnemies[enemyID];
    }
}
