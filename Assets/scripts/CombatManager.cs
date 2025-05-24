using Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager instance;

    public List<GameObject> combatOrder;
    int currentCombatIndex = 0;

    public IEnumerator StartCombat()
    {

        yield return new WaitForSeconds(1f);

        if (combatOrder[currentCombatIndex] == null)
        {
            GameObject Target = combatOrder[currentCombatIndex];
            if (Target.GetComponent<SetEnemyUI>() != null)
            {
                Target.GetComponent<EnemyTurn>().StartTurn();
            }
            else
            {

            }
            currentCombatIndex = (currentCombatIndex + 1) % combatOrder.Count; // moves to the next in the list and wraps around
        }
    }

}
