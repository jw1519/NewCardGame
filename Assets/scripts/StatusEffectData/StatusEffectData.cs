using UnityEngine;

[CreateAssetMenu(fileName = "New Status Effect", menuName = "Status Effect")]
public class StatusEffectData : ScriptableObject
{
    public string effectName;
    public int DOTAmount;
    public int duration;

    public GameObject effectIcon;
}
