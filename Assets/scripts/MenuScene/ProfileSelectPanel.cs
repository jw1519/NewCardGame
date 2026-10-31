using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class ProfileSelectPanel : BasePanel
{
    public List<GameObject> buttons;
    public void Start()
    {
        List<ProfileData> datas = SaveManager.Instance.GetProfileDatas();
        Debug.Log(datas.Count);
        for (int i = 0; i < datas.Count - 1; i++)
        {
            Debug.Log(datas[i].profileName);
            buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = datas[i].profileName;
        }
    }
}
