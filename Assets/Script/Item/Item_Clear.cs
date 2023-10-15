using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Item_Clear : ItemBase
{
    protected override void Awake()
    {
        base.Awake();
        itemID = 0;
    }
}
