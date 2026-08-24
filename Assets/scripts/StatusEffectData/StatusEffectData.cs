using UnityEngine;

[CreateAssetMenu(fileName = "New Status Effect", menuName = "Status Effect")]
public class StatusEffectData : ScriptableObject
{
    public string effectName;
    public int DOTAmount;
    public int duration;
    public bool doesDamage;

    public virtual void ApplyEffect(GameObject target)
    {
        Debug.Log("Applying effect: " + effectName);
    }
    public virtual void RemoveEffect(GameObject target)
    {
        Debug.Log("Removing effect: " + effectName);
    }


    public GameObject effectIcon;
}
