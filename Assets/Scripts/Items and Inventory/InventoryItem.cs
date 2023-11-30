using System;

[Serializable]
public class InventoryItem
{
    public ItemData data;
    public int stackSize;

    // Thêm vật phẩm vào số lượng stack
    public InventoryItem(ItemData _newItemData)
    {
        data = _newItemData;
        AddStack();
    }


    public void AddStack() => stackSize++;
    public void RemoveStack() => stackSize--;
}
