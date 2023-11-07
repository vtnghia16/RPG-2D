using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject_Trigger : MonoBehaviour
{
    private ItemObject myItemObject => GetComponentInParent<ItemObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            // Nếu nhân vật chết quái vật không nhặt vật phẩm 
            if (collision.GetComponent<CharacterStats>().isDead)
                return;

            Debug.Log("Vật phẩm nhặt được ");
            myItemObject.PickupItem();
        }
    }
}
