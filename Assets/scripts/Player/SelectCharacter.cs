using Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacter : MonoBehaviour
{
    public void OnClick()
    {
        SelectManager.instance.SelectPlayer(gameObject);
    }
}
