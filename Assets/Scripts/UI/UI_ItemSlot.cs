using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class UI_ItemSlot : MonoBehaviour , IPointerDownHandler ,IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] protected Image itemImage;
    [SerializeField] protected TextMeshProUGUI itemText;

    protected UI ui;
    public InventoryItem item;

    protected virtual void Start()
    {
        ui = GetComponentInParent<UI>();
    }

    // Cập nhật các ô chứa của vật phẩm trong inventory
    public void UpdateSlot(InventoryItem _newItem)
    {
        item = _newItem;

        itemImage.color = Color.white;

        if (item != null)
        {
            itemImage.sprite = item.data.itemIcon;

            // Nếu có SL stack thì hiển thị amount
            if (item.stackSize > 1)
            {
                itemText.text = item.stackSize.ToString();
            }
            else
            {
                itemText.text = "";
            }
        }
    }

    // Ẩn vật phẩm khỏi kho
    public void CleanUpSlot()
    {
        item = null;

        itemImage.sprite = null;
        itemImage.color = Color.clear;
        itemText.text = "";
    }

    // Xóa vật phẩm ra khỏi kho
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (item == null)
            return;

        ui.itemToolTip.HideToolTip();

        if (Input.GetKey(KeyCode.LeftControl))
        {
            Inventory.instance.RemoveItem(item.data);
            return;
        }

        // Chọn vật phẩm thêm vào nhân vật
        if (item.data.itemType == ItemType.Equipment)
            Inventory.instance.EquipItem(item.data);

    }

    // Hiển thị thông báo ghi rê chuột
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item == null)
            return;

        ui.itemToolTip.ShowToolTip(item.data as ItemData_Equipment);
    }

    // Tắt thông báo
    public void OnPointerExit(PointerEventData eventData)
    {
        if (item == null)
            return;

        ui.itemToolTip.HideToolTip();
    }
}
