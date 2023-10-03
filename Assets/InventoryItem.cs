using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryItem 
{
    public ItemData data;
    public int stackSize; // Số lượng vật phẩm khi nhân vật ăn được thêm vào stack

    public InventoryItem(ItemData _newItemData)
    {
        data = _newItemData;
        AddStack();
    }

    public void AddStack() => stackSize++;

    public void RemoveStack() => stackSize--;
}
