using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class CombatLog : MonoBehaviour
{
    public TextMeshProUGUI logText; // Drag your CombatLog TextMeshPro component here in the Inspector

    public void UpdateLog(string message)
    {
        logText.text = message;
    }
}
