using NUnit.Framework;
using UnityEngine;
using Enemy;
using System.Collections.Generic;
public class CombatRoom : BaseRoom
{
    public List<BaseEnemy> enemies; // List of enemies in this combat room, set in the inspector
    public override void EnterRoom()
    {
        if (!isCleared)
        {
            // Start combat encounter
            Debug.Log("Entering Combat Room at (" + x + "," + y + ")");
            // Here you would trigger the combat system, spawn enemies, etc.
            // For now, we'll just mark the room as cleared for testing purposes
            isCleared = true;
            // After combat is resolved, you can call a method to handle rewards, etc.
        }
        else
        {
            Debug.Log("This Combat Room has already been cleared.");
        }
    }
}
