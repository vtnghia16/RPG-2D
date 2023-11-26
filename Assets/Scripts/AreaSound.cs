using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSound : MonoBehaviour
{
    // Giới hạn vùng va chạm của quái vật
    [SerializeField] private int areaSoundIndex;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
            AudioManager.instance.PlaySFX(areaSoundIndex,null);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
            AudioManager.instance.StopSFXWithTime(areaSoundIndex);
    }
}
