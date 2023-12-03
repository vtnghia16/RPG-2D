using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour , ISaveManager
{
    public static PlayerManager instance;
    public Player player;

    public int score;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        //score += 10;
    }

    public int GetCurrency() => score;

    public void LoadData(GameData _data)
    {
        this.score = _data.Score;
    }

    public void SaveData(ref GameData _data)
    {
        _data.Score = this.score;
    }

    public static implicit operator PlayerManager(CharacterStats v)
    {
        throw new NotImplementedException();
    }
}
