using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour 
{
    public static SaveManager instance;

    [SerializeField] private string fileName;
    [SerializeField] private string filePath = "D:\\GameDev";
    [SerializeField] private bool encryptData;

    private GameData gameData;
    [SerializeField] private List<ISaveManager> saveManagers;
    private FileDataHandler dataHandler;


    [ContextMenu("Delete save file")]
    public void DeleteSavedData()
    {
        dataHandler = new FileDataHandler(filePath, fileName, encryptData);
        dataHandler.Delete();

    }

    [Header("Save scene")]
    public int sceneBuildIndex;

    private void OnTriggerEnter2D(Collider2D other)
    {
        print("Trigger Entered");

        SaveGame();

        if (other.tag == "Player")
        {
            print("Switch sene to " + sceneBuildIndex);
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
        }
    }

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }


    private void Start()
    {
        dataHandler = new FileDataHandler(filePath, fileName, encryptData);

        // Tìm tất cả file quản lý lưu
        saveManagers = FindAllSaveManagers();

        //Invoke("LoadGame", .05f);
        
        LoadGame();
        
        
    }

    // Tạo data game mới
    public void NewGame()
    {
        gameData = new GameData();
    }

    // Load data từ game
    public void LoadGame()
    {
        gameData = dataHandler.Load();

        // Check file có lưu data nếu không thì new game
        if (this.gameData == null)
        {
            Debug.Log("Không tìm thấy dữ liệu đã lưu!");
            NewGame();
        }

        // Xử lý dữ liệu để load data
        foreach (ISaveManager saveManager in saveManagers)
        {
            saveManager.LoadData(gameData);
        }
    }


    // Lưu data vào file
    public void SaveGame()
    {
        // Xử lý dữ liệu để lưu data
        foreach(ISaveManager saveManager in saveManagers)
        {
            saveManager.SaveData(ref gameData);
        }

        dataHandler.Save(gameData);
    }

    // Thoát khỏi app khi đã lưu data
    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<ISaveManager> FindAllSaveManagers()
    {
        IEnumerable<ISaveManager> saveManagers = FindObjectsOfType<MonoBehaviour>().OfType<ISaveManager>();

        return new List<ISaveManager>(saveManagers);
    }

    public bool HasSavedData()
    {
        if (dataHandler.Load() != null)
        {
            return true;
        }

        return false;
    }
}
