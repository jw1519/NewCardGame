using UnityEngine;

[CreateAssetMenu(fileName = "New Status Effect", menuName = "Status Effect/ DOT Effect")]
public class StatusEffectData : ScriptableObject
{
    public string effectName;
    public float DOTAmount;
    public int duration;
    public bool doesDamage;
    public string description;
    public Sprite effectSprite;

    public virtual void ApplyEffect(GameObject target)
    {
    }
    public virtual void RemoveEffect()
    {
    }
}
