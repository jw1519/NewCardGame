using Character;
using TMPro;
public class GameWonPanel : BasePanel
{
    public int goldEarned;
    public TextMeshProUGUI goldEarnedText;

    public void UpdateStats()
    {
        goldEarnedText.text = "Gold Earned " + goldEarned.ToString();
        FindObjectOfType<BaseCharacter>().gold += goldEarned;
    }

}
