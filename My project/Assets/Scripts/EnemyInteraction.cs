using UnityEngine;

public class EnemyInteraction : MonoBehaviour
{
    public string combatSceneName = "CombatScene";

    void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Player"))
    {
        if (GameManager.Instance != null)
        {
            // Store the original enemy for destruction after combat
            GameManager.Instance.originalEnemy = this.gameObject;

            // Preserve the original enemy temporarily
            DontDestroyOnLoad(GameManager.Instance.originalEnemy);

            // Clone the enemy for combat
            GameObject enemyClone = Instantiate(this.gameObject);
            DontDestroyOnLoad(enemyClone); // Persist the clone across scenes
            GameManager.Instance.enemyPrefab = enemyClone;
            GameManager.Instance.currentEnemyName = this.gameObject.name;

            Debug.Log($"Assigned original enemy: {GameManager.Instance.originalEnemy.name}");
        }
        else
        {
            Debug.LogError("GameManager instance is null!");
        }

        // Load the combat scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(combatSceneName);
    }
}


}
