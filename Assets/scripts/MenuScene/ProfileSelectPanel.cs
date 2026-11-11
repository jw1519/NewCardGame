using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ProfileSelectPanel : BasePanel
{
    public Button[] buttons;
    FileDataHandler fileHandler;
    private void Start()
    {
        if (SaveManager.Instance.selectedProfile == null)
        {
            ClosePanel();
        }
    }
    public void OnEnable()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (SlotExist(i))
            {
                buttons[i].gameObject.name = i.ToString();
                buttons[i].transform.GetComponentInChildren<TextMeshProUGUI>().text = "Profile " + i.ToString();
            }
        }
    }
    private bool SlotExist(int slot)
    {
        fileHandler = null;
        fileHandler = new FileDataHandler(Application.persistentDataPath, "Profile " + slot);
        if (fileHandler.LoadProfile() == null)
        {
            return false;
        }
        else
            return true;

    }
}
