using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public enum EquipmentType
{
    Weapon,
    Armor,
    Amulet,
    Flask
}

// Trang bị vật phẩm vào người chơi
[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Equipment")]
public class ItemData_Equipment : ItemData
{
    public EquipmentType equipmentType;

    [Header("Unique effect")]
    public float itemCooldown;
    public ItemEffect[] itemEffects;

    [Header("Stats")]
    public int damage;
    public int health;
    public int armor;

    [Header("Craft requirements")]
    
    public List<InventoryItem> craftingMaterials;

    private int descriptionLength;

    public void Effect(Transform _enemyPosition)
    {
        foreach (var item in itemEffects)
        {
            item.ExecuteEffect(_enemyPosition);
        }
    }

    // Chỉnh sửa chỉ số khi trang bị vật phẩm
    public void AddModifiers()
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        playerStats.damage.AddModifier(damage);
        playerStats.maxHealth.AddModifier(health);
        playerStats.armor.AddModifier(armor);

    }

    // Xóa các chỉ số
    public void RemoveModifiers() 
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        playerStats.damage.RemoveModifier(damage);
        playerStats.maxHealth.RemoveModifier(health);
        playerStats.armor.RemoveModifier(armor);

    }

    public override string GetDescription()
    {
        sb.Length = 0;
        descriptionLength = 0;


        AddItemDescription(damage, "Damage");
        AddItemDescription(health, "Health");
        AddItemDescription(armor, "Armor");


        for (int i = 0; i < itemEffects.Length; i++)
        {
            if (itemEffects[i] != null && !string.IsNullOrEmpty(itemEffects[i].effectDescription))
            {
                sb.AppendLine();
                sb.AppendLine("Mô tả: " + itemEffects[i].effectDescription);
                descriptionLength++;
            }
        }



        if (descriptionLength < 5)
        {
            for (int i = 0; i < 5 - descriptionLength; i++)
            {
                sb.AppendLine();
                sb.Append("");
            }
        }

        return sb.ToString();
    }



    private void AddItemDescription(int _value, string _name)
    {
        if (_value != 0)
        {
            if (sb.Length > 0)
                sb.AppendLine();

            if (_value > 0)
                sb.Append("+ " + _value + " " + _name);

            descriptionLength++;
        }       
    }
}
