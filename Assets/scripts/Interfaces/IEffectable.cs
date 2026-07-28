public interface IEffectable
{
    public void ApplyEffect(StatusEffectData data);
    public void UpdateEffect();
    public void RemoveEffect(string name);
    public StatusEffectData GetEffect(string name);
}
