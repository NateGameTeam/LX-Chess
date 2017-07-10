using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {
    public GameObject MainMenuCanvas;
    public GameObject BoardMenuCanvas;
    public event Action BeforeSceneUnload;
    public event Action AfterSceneLoad;


    public CanvasGroup faderCanvasGroup;
    public float fadeDuration = 1f;
    public string startingSceneName = "SecurityRoom";
    public string initialStartingPositionName = "DoorToMarket";
    public SaveData playerSaveData;
    public void newGame()
    {
        SceneManager.LoadSceneAsync("Main", LoadSceneMode.Additive);
        SwitchMenu();
    }

    public void LoadGame()
    {
        StartCoroutine(LoadSceneAndLoadSaveData());
        SwitchMenu();
    }
    private void Start()
    {
        BoardMenuCanvas.SetActive(false);

    }
    private IEnumerator LoadSceneAndLoadSaveData()
    {
        yield return SceneManager.LoadSceneAsync("Main", LoadSceneMode.Additive);
        Scene newlyLoadedScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(newlyLoadedScene);
        SaveDataManager.Load();

        SwitchMenu();
    }

    public void LoadScene(string sceneName)
    {
        print(sceneName);
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
    }

    public void ReturnMainMenu()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(SceneManager.sceneCount - 1));
        MainMenuCanvas.SetActive(true);
        BoardMenuCanvas.SetActive(false);
    }
    public void Quit()
    {
        Application.Quit();
    }
    private void SwitchMenu()
    {
        MainMenuCanvas.SetActive(false);
        BoardMenuCanvas.SetActive(true);
    }

}
