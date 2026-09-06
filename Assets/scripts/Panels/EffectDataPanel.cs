using System.Collections.Generic;
using UnityEngine;

public class EffectDataPanel : BasePanel
{
    List<StatusEffectDetailsUI> statusEffectDetailsUIs = new List<StatusEffectDetailsUI>();
    public GameObject effectDataPanel;
    public Transform content;

    public void OnEnable()
    {
        if (statusEffectDetailsUIs == null)
            return;
        for (int i = 0; i < statusEffectDetailsUIs.Count; i++)
        {
            if (statusEffectDetailsUIs[i] == null)
            {
                statusEffectDetailsUIs.RemoveAt(i);
                i--;
                return;
            }
            statusEffectDetailsUIs[i].UpdateUI();
                
        }
    }
    public void AddEffectUI(StatusEffectData statusEffectData)
    {
        if (statusEffectDetailsUIs != null && statusEffectDetailsUIs.Find(p => p.StatusEffectData == statusEffectData))
        {
            StatusEffectDetailsUI effect = statusEffectDetailsUIs.Find(p => p.StatusEffectData == statusEffectData);
            effect.SetStatusEffectData(statusEffectData);
            return;
        }
        StatusEffectDetailsUI newEffectUI = Instantiate(effectDataPanel, content).GetComponent<StatusEffectDetailsUI>();
        newEffectUI.SetStatusEffectData(statusEffectData);
        statusEffectDetailsUIs.Add(newEffectUI);
    }
}
