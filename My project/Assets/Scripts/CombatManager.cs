using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatManager : MonoBehaviour
{
    public GameObject player; // Reference to the player GameObject
    public GameObject enemy; // Local reference to the instantiated enemy
    public Transform spawnPoint; // Where the enemy should spawn in the combat scene

    public CombatLog combatLog; // Reference to a script managing combat log UI
    public GameObject floatingTextPrefab; // Prefab for showing floating damage numbers

    public string explorationSceneName = "3DScene"; // Name of the exploration scene

    private bool playerTurn = true;
    private bool combatEnded = false;

    void Start()
    {
        // Ensure the GameManager and enemy prefab exist
        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.enemyPrefab != null)
            {
                // Place the cloned enemy at the spawn point
                enemy = GameManager.Instance.enemyPrefab;
                enemy.transform.position = spawnPoint.position;
                enemy.transform.rotation = Quaternion.identity;

                Debug.Log($"Instantiated enemy: {enemy.name} at {spawnPoint.position}");
            }
            else
            {
                Debug.LogError("No enemy prefab assigned in GameManager.");
            }
        }
        else
        {
            Debug.LogError("GameManager instance is null in CombatManager.");
        }
    }

    void Update()
    {
        if (combatEnded) return;

        if (playerTurn && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(PlayerAttack());
        }
    }

    IEnumerator PlayerAttack()
    {
        // Validate player and enemy references
        if (player == null || player.GetComponent<PlayerHealth>() == null)
        {
            Debug.LogError("Player or PlayerHealth is null!");
            yield break;
        }

        if (enemy == null || enemy.GetComponent<EnemyHealth>() == null)
        {
            Debug.LogError("Enemy or EnemyHealth is null!");
            yield break;
        }

        if (enemy.GetComponent<FlashEffect>() == null)
        {
            Debug.LogError("FlashEffect is missing on the enemy!");
            yield break;
        }

        // Execute player attack
        int playerAttack = player.GetComponent<PlayerHealth>().attack;
        combatLog.UpdateLog($"Player attacks Enemy for {playerAttack} damage!");
        enemy.GetComponent<FlashEffect>().Flash();
        SpawnFloatingText(enemy.transform.position, playerAttack);
        enemy.GetComponent<EnemyHealth>().TakeDamage(playerAttack);

        yield return new WaitForSeconds(1f);

        if (!IsCombatOver())
        {
            playerTurn = false;
            StartCoroutine(EnemyTurn());
        }
        else
        {
            combatEnded = true;
            HandleEnemyDefeat();
        }
    }

    IEnumerator EnemyTurn()
    {
        if (enemy != null && enemy.GetComponent<EnemyHealth>().health > 0)
        {
            if (player != null && player.GetComponent<PlayerHealth>() != null)
            {
                int enemyAttack = enemy.GetComponent<EnemyHealth>().attack;
                combatLog.UpdateLog($"Enemy attacks Player for {enemyAttack} damage!");
                player.GetComponent<FlashEffect>().Flash();
                SpawnFloatingText(player.transform.position, enemyAttack);
                player.GetComponent<PlayerHealth>().TakeDamage(enemyAttack);
            }

            yield return new WaitForSeconds(1f);

            if (!IsCombatOver())
            {
                playerTurn = true;
            }
            else
            {
                combatEnded = true;
            }
        }
    }

    bool IsCombatOver()
    {
        if (player == null || player.GetComponent<PlayerHealth>().health <= 0)
        {
            combatLog.UpdateLog("Player has been defeated! Combat Ends.");
            return true;
        }

        if (enemy == null || enemy.GetComponent<EnemyHealth>().health <= 0)
        {
            combatLog.UpdateLog("Enemy has been defeated! Combat Ends.");
            return true;
        }

        return false;
    }

    void HandleEnemyDefeat()
{
    if (enemy == null)
    {
        Debug.LogError("Enemy is null in HandleEnemyDefeat.");
        return;
    }

    if (GameManager.Instance == null)
    {
        Debug.LogError("GameManager instance is null.");
        return;
    }

    // Mark the enemy as defeated
    EnemyID enemyIDComponent = enemy.GetComponent<EnemyID>();
    if (enemyIDComponent == null)
    {
        Debug.LogError("EnemyID component is missing on the enemy GameObject.");
        return;
    }

    string enemyID = enemyIDComponent.enemyID;
    GameManager.Instance.MarkEnemyAsDefeated(enemyID);

    // Destroy the original enemy in the 3D world
    if (GameManager.Instance.originalEnemy != null)
{
    Debug.Log($"Forcefully destroying original enemy: {GameManager.Instance.originalEnemy.name}");
    
    // Destroy all children and components
    foreach (Transform child in GameManager.Instance.originalEnemy.transform)
    {
        DestroyImmediate(child.gameObject);
    }

    // Destroy the main GameObject
    DestroyImmediate(GameManager.Instance.originalEnemy);
    GameManager.Instance.originalEnemy = null; // Clear the reference
}
    else
    {
        Debug.LogError("GameManager.Instance.originalEnemy is null. Could not destroy original enemy.");
    }

    // Return to the exploration scene
    combatLog.UpdateLog("Returning to the exploration scene...");
    SceneManager.LoadScene(explorationSceneName);
}


    void SpawnFloatingText(Vector3 position, int damage)
    {
        if (floatingTextPrefab != null)
        {
            GameObject floatingText = Instantiate(floatingTextPrefab, position + Vector3.up, Quaternion.identity);
            TextMesh textMesh = floatingText.GetComponent<TextMesh>();
            if (textMesh != null)
            {
                textMesh.text = damage.ToString();
            }
            else
            {
                Debug.LogError("FloatingTextPrefab is missing a TextMesh component!");
            }
        }
        else
        {
            Debug.LogError("FloatingTextPrefab is not assigned in the CombatManager.");
        }
    }
}
