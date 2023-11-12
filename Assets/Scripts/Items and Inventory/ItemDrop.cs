using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] private int possibleItemDrop; // Số lượng cơ hội rơi vật liệu
    [SerializeField] private ItemData[] possibleDrop;
    private List<ItemData> dropList = new List<ItemData>();

    [SerializeField] private GameObject dropPrefab; // Prefab

    // Cho vật liệu tơi rớt ngẫu nhiên theo % cơ hội
    public virtual void GenerateDrop()
    {
        if (possibleDrop.Length <= 0)
            return;

        for (int i = 0; i < possibleDrop.Length; i++)
        {
            if (Random.Range(0, 100) <= possibleDrop[i].dropChance)
                dropList.Add(possibleDrop[i]);
        }


        for (int i = 0; i < possibleItemDrop; i++)
        {
            if (dropList.Count <= 0)
                return;

            ItemData randomItem = dropList[Random.Range(0, dropList.Count - 1)];

            dropList.Remove(randomItem);
            DropItem(randomItem);

        }
    }


    // Xử lý rớt vật phẩm
    protected void DropItem(ItemData _itemData)
    {
        // Vị trí thả của vật phẩm 
        GameObject newDrop = Instantiate(dropPrefab, transform.position, Quaternion.identity);

        // Set vận tốc thả của mỗi đồ vật là random
        Vector2 randomVelocity = new Vector2(Random.Range(-5, 5), Random.Range(15, 20));


        newDrop.GetComponent<ItemObject>().SetupItem(_itemData, randomVelocity);
    }
}
