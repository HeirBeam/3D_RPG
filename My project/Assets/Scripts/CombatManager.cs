using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatManager : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;

    public CombatLog combatLog;
    public GameObject floatingTextPrefab;

    public string explorationSceneName = "3DScene"; // Name of the 3D exploration scene
    private bool playerTurn = true;
    private bool combatEnded = false;

    void Start()
    {
        combatLog.UpdateLog("Combat Started!");
    }

    void Update()
    {
        if (combatEnded)
        {
            return; // Stop further execution if combat has ended
        }

        if (playerTurn)
        {
            if (Input.GetKeyDown(KeyCode.Space)) // Wait for the player to press space
            {
                StartCoroutine(PlayerAttack()); // Start the player attack coroutine
            }
        }
    }

    IEnumerator PlayerAttack()
    {
        if (enemy != null)
        {
            int playerAttack = player.GetComponent<PlayerHealth>().attack; // Get player's attack value
            combatLog.UpdateLog($"Player attacks Enemy for {playerAttack} damage!");

            // Visual effects
            enemy.GetComponent<FlashEffect>().Flash();
            SpawnFloatingText(enemy.transform.position, playerAttack);

            enemy.GetComponent<EnemyHealth>().TakeDamage(playerAttack); // Deal damage to the enemy

            yield return new WaitForSeconds(1f); // Wait for the attack to finish

            if (!IsCombatOver())
            {
                playerTurn = false; // Switch to enemy turn if combat isn't over
                StartCoroutine(EnemyTurn());
            }
            else
            {
                combatEnded = true; // End combat if necessary
                HandleEnemyDefeat(); // Transition back to 3D scene
            }
        }
    }

    IEnumerator EnemyTurn()
    {
        if (enemy != null && enemy.GetComponent<EnemyHealth>().health > 0)
        {
            if (player != null)
            {
                int enemyAttack = enemy.GetComponent<EnemyHealth>().attack; // Get enemy's attack value
                combatLog.UpdateLog($"Enemy attacks Player for {enemyAttack} damage!");

                // Visual effects
                player.GetComponent<FlashEffect>().Flash();
                SpawnFloatingText(player.transform.position, enemyAttack);

                player.GetComponent<PlayerHealth>().TakeDamage(enemyAttack); // Deal damage to the player
            }

            yield return new WaitForSeconds(1f); // Wait for the enemy's attack to finish

            if (!IsCombatOver())
            {
                playerTurn = true; // Switch back to player's turn
            }
            else
            {
                combatEnded = true; // End combat if necessary
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
        // Mark the enemy as defeated in the GameManager
        if (GameManager.Instance != null)
        {
            GameManager.Instance.enemyDefeated = true;    
        }

        // Return to the 3D exploration scene
        combatLog.UpdateLog("Returning to the exploration scene...");
        SceneManager.LoadScene(explorationSceneName);
    }

    void SpawnFloatingText(Vector3 position, int damage)
    {
        if (floatingTextPrefab != null)
        {
            GameObject floatingText = Instantiate(floatingTextPrefab, position + Vector3.up, Quaternion.identity);
            floatingText.GetComponent<TextMesh>().text = damage.ToString();
        }
    }
}