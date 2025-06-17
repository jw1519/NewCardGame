using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPack : MonoBehaviour
{
    public int packCost;

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Buy()
    {
        ShopManager.instance.Buy(packCost);
    }
}
