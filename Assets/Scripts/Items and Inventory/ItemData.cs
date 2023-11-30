using System.Text;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

// Lưu các loại item
public enum ItemType
{
    Material,
    Equipment
}


[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Item")]
public class ItemData : ScriptableObject 
{
    public ItemType itemType;
    public string itemName;
    public Sprite itemIcon;
    public string itemId; // Lưu vật phẩm theo ID

    [Range(0,100)]
    public float dropChance; // Cơ hội rớt vật liệu

    protected StringBuilder sb = new StringBuilder();

    // Lưu các vật phẩm vào DB, mỗi vật phẩm sẽ có 1 itemID
    private void OnValidate()
    {
#if UNITY_EDITOR
        string path = AssetDatabase.GetAssetPath(this);
        itemId = AssetDatabase.AssetPathToGUID(path);
#endif
    }

    public virtual string GetDescription()
    {
        return "";
    }
}
