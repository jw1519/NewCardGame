using TMPro;
using UnityEngine;

public class SeedPanel : BasePanel
{
    [SerializeField] TMP_InputField seedInputField;
    public void GenerateRandomSeed()
    {
        int randomSeed = Random.Range(int.MinValue, int.MaxValue);
        seedInputField.text = randomSeed.ToString();
    }
}
