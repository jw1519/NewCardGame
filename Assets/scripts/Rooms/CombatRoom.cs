using NUnit.Framework;
using UnityEngine;
using Enemy;
using System.Collections.Generic;
public class CombatRoom : BaseRoom
{
    public List<BaseEnemy> enemies; // List of enemies in this combat room, set in the inspector
    public override void EnterRoom()
    {

    }
}
