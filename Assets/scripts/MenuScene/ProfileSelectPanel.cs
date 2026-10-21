using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class ProfileSelectPanel : MonoBehaviour
{
    public List<GameObject> buttons;
    private void Start()
    {
        List<ProfileData> datas = SaveManager.Instance.GetProfileDatas();
        for (int i = 0; i < datas.Count - 1; i++)
        {
            buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = datas[i].profileName;
            buttons[i].GetComponent<Button>().onClick.RemoveAllListeners();
            buttons[i].GetComponent<Button>().onClick.AddListener(buttons[i].GetComponent<ProfileButton>().LoadProfile);
        }
    }
}
