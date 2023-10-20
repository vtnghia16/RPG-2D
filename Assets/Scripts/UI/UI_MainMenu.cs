using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainMenu : MonoBehaviour
{
    [SerializeField] private string sceneName = "MainScene";
    [SerializeField] private GameObject continueButton;
    [SerializeField] UI_FadeScreen fadeScreen;

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
        // SceneManager.LoadScene(sceneName);
        StartCoroutine(LoadSceneWithFadeEffect(1.5f));
    }

    public void NewGame()
    {
        SaveManager.instance.DeleteSavedData();
        //SceneManager.LoadScene(sceneName);
        StartCoroutine(LoadSceneWithFadeEffect(1.5f));

    }

    public void ExitGame()
    {
        Debug.Log("Exit game");
        // Application.Quit();
    }

    IEnumerator LoadSceneWithFadeEffect(float _delay)
    {
        fadeScreen.FadeOut();

        yield return new WaitForSeconds(_delay);

        SceneManager.LoadScene(sceneName);
    }
}
