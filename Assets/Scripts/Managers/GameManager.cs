using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour, ISaveManager
{

    public static GameManager instance;

    private Transform player;

    [SerializeField] private Checkpoint[] checkpoints;
    [SerializeField] private string closestCheckpointId;

    [Header("Lost score")]
    [SerializeField] private GameObject lostScorePrefab;
    public int lostScoreAmount;
    [SerializeField] private float lostScoreX;
    [SerializeField] private float lostScoreY;
    private bool pasuedGame;

    [Header("Show text")]
    public Text displayTextPause;
    public string textToShowPause = "Key Pressed!";


    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        checkpoints = FindObjectsOfType<Checkpoint>();

        player = PlayerManager.instance.player.transform;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
            RestartScene();
 

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!pasuedGame)
            {
                pasuedGame = true;
                GameManager.instance.PauseGame(pasuedGame);
                displayTextPause.text = textToShowPause;
            }
            else
            {
                pasuedGame = false;
                GameManager.instance.PauseGame(pasuedGame);
                displayTextPause.text = "";
            }

        }
    }

    [SerializeField] private string sceneName = "MainScene";

    public void RestartScene()
    {
        SaveManager.instance.SaveGame();
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(sceneName);
    }

    

    public void LoadData(GameData _data) => StartCoroutine(LoadWithDelay(_data));

    private void LoadCheckpoints(GameData _data)
    {
        foreach (KeyValuePair<string, bool> pair in _data.checkpoints)
        {
            foreach (Checkpoint checkpoint in checkpoints)
            {
                if (checkpoint.id == pair.Key && pair.Value == true)
                    checkpoint.ActivateCheckpoint();
            }
        }
    }

    private void LoadLostScore(GameData _data)
    {
        lostScoreAmount = _data.lostScoreAmount;
        lostScoreX = _data.lostScoreX;
        lostScoreY = _data.lostScoreY;

        if (lostScoreAmount > 0)
        {
            GameObject newLostCurrency = Instantiate(lostScorePrefab,new Vector3(lostScoreX,lostScoreY),Quaternion.identity);
            newLostCurrency.GetComponent<LostCurrencyController>().currency = lostScoreAmount;
        }

        lostScoreAmount = 0;
    }

    private IEnumerator LoadWithDelay(GameData _data)
    {
        yield return new WaitForSeconds(.1f);

        LoadCheckpoints(_data);
        LoadClosestCheckpoint(_data);
        LoadLostScore(_data);
    }

    public void SaveData(ref GameData _data)
    {
        _data.lostScoreAmount = lostScoreAmount;
        _data.lostScoreX = player.position.x;
        _data.lostScoreY = player.position.y;


        if(FindClosestCheckpoint() != null)
            _data.closestCheckpointId = FindClosestCheckpoint().id;

        _data.checkpoints.Clear();

        foreach (Checkpoint checkpoint in checkpoints)
        {
            _data.checkpoints.Add(checkpoint.id, checkpoint.activationStatus);
        }
    }
    private void LoadClosestCheckpoint(GameData _data)
    {
        if (_data.closestCheckpointId == null)
            return;


        closestCheckpointId = _data.closestCheckpointId;

        foreach (Checkpoint checkpoint in checkpoints)
        {
            if (closestCheckpointId == checkpoint.id)
                player.position = checkpoint.transform.position;
        }
    }

    private Checkpoint FindClosestCheckpoint()
    {
        float closestDistance = Mathf.Infinity;
        Checkpoint closestCheckpoint = null;

        foreach (var checkpoint in checkpoints)
        {
            float distanceToCheckpoint = Vector2.Distance(player.position, checkpoint.transform.position);

            if (distanceToCheckpoint < closestDistance && checkpoint.activationStatus == true)
            {
                closestDistance = distanceToCheckpoint;
                closestCheckpoint = checkpoint;
            }
        }

        return closestCheckpoint;
    }


    public void PauseGame(bool _pause)
    {
        if (_pause)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
}
