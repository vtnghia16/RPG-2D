using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Các phim hotkey để đánh các mục tiêu trong phạm vi blackHole
public class Blackhole_HotKey_Controller : MonoBehaviour
{
    private SpriteRenderer sr;
    private KeyCode myHotKey;
    private TextMeshProUGUI myText;

    private Transform myEnemy;
    private Blackhole_Skill_Controller blackHole;

    public void SetupHotKey(KeyCode _myNewHotKey,Transform _myEnemy, Blackhole_Skill_Controller _myBlackHole)
    {
        sr = GetComponent<SpriteRenderer>();
        myText = GetComponentInChildren<TextMeshProUGUI>();

        myEnemy = _myEnemy;
        blackHole = _myBlackHole;

        myHotKey = _myNewHotKey;
        myText.text = _myNewHotKey.ToString();
    }

    private void Update()
    {
        // Set thực hiện các phím tấn công mục tiêu
        if (Input.GetKeyDown(myHotKey))
        {
            // Tấn công các mục tiêu quái vật đã được chỉ định thông qua hotKey
            blackHole.AddEnemyToList(myEnemy);

            myText.color = Color.clear;
            sr.color = Color.clear;
        }
    }
}
