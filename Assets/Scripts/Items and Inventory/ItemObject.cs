using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Vật phẩm của quái vật khi rơi
public class ItemObject : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private ItemData itemData;

    private void SetupVisuals()
    {
        if (itemData == null)
            return;

        // Hiển thị tên vật liệu khi nhặt
        GetComponent<SpriteRenderer>().sprite = itemData.itemIcon;
        gameObject.name = "Item object - " + itemData.itemName;
    }

    // Setup các vật phẩm
    public void SetupItem(ItemData _itemData, Vector2 _velocity)
    {
        itemData = _itemData;
        rb.velocity = _velocity;

        SetupVisuals();
    }

    // Nhặt vật phẩm vào kho đồ
    public void PickupItem()
    {
        if (!Inventory.instance.CanAddItem() && itemData.itemType == ItemType.Equipment)
        {
            rb.velocity = new Vector2(0, 7);
            PlayerManager.instance.player.fx.CreatePopUpText("Inventory is full");
            return;
        }

        AudioManager.instance.PlaySFX(9, transform);
        Inventory.instance.AddItem(itemData);
        Destroy(gameObject);
    }
}
