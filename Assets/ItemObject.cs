using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    private SpriteRenderer sr;

    [SerializeField] private ItemData itemData;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        // Hiện vật phẩm
        sr.sprite = itemData.icon;
    }

    // Nhặt vật phẩm cho nhân vật
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null)
        {
            Debug.Log("Picked up item " + itemData.itemName);
            Destroy(gameObject);
        }
    }
}
