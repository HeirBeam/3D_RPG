using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInteraction : MonoBehaviour
{
    public string combatSceneName = "CombatScene";

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Transition to the 2D combat scene
            UnityEngine.SceneManagement.SceneManager.LoadScene(combatSceneName);
        }
    }
}
