using Item;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class TreasureRoom : BaseRoom
{
    public List<BaseItem> treasureItems; // List of items in the treasure room, set in the inspector

    public override void EnterRoom()
    {
        ClearRoom();
    }
}
