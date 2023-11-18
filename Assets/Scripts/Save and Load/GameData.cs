using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int Score;


    public SerializableDictionary<string, int> inventory;
    public List<string> equipmentId;


    public SerializableDictionary<string, bool> checkpoints;
    public string closestCheckpointId;

    public float lostScoreX;
    public float lostScoreY;
    public int lostScoreAmount;

    public SerializableDictionary<string, float> volumeSettings;

    public GameData()
    {
        this.lostScoreX = 0;
        this.lostScoreY = 0;
        this.lostScoreAmount = 0;


        this.Score = 0;

        inventory = new SerializableDictionary<string, int>();
        equipmentId = new List<string>();

        closestCheckpointId = string.Empty;
        checkpoints = new SerializableDictionary<string, bool>();

        volumeSettings= new SerializableDictionary<string, float>();
    }
}
