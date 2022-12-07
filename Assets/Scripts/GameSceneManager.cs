using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField] private int startSceneIndex;
    [SerializeField] private List<string> scenes;
    [HideInInspector] public string currentScene;

    public void LoadStaringScene()
    {
        LoadScene(scenes[startSceneIndex], false);
    }

    public void ReloadScene()
    {
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(currentScene);
        UnityEngine.SceneManagement.SceneManager.LoadScene(currentScene, UnityEngine.SceneManagement.LoadSceneMode.Additive);
    }

    public void LoadScene(string sceneName, bool unloadCurrent = true)
    {
        if (currentScene == sceneName)
        {
            Debug.Log("Scene already loaded");
            return;
        }
        try
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName, UnityEngine.SceneManagement.LoadSceneMode.Additive);
        }
        catch (System.Exception ex)
        {
            Debug.Log("Scene can't be loaded " + ex);
            throw;
        }

        if (unloadCurrent)
        {
            UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(currentScene);
        }
        currentScene = sceneName;
    }
}