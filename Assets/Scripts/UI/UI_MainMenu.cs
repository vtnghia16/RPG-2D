using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainMenu : MonoBehaviour
{
    [SerializeField] private string sceneName = "MainScene";
    [SerializeField] private GameObject continueButton;

    public void Start()
    {
        if(SaveManager.instance.HasSavedData() == false)
        {
            continueButton.SetActive(false);
        }
    }

    public void ContinueGame()
    {
        // V�o m�n h�nh gameScene
        SceneManager.LoadScene(sceneName);
    }

    public void NewGame()
    {
        SaveManager.instance.DeleteSavedData();
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Debug.Log("Exit game");
        // Application.Quit();
    }
}
