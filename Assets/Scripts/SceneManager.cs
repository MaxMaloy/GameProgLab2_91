using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField] private int startSceneIndex;
    [SerializeField] private List<string> scenes;
    private string currentScene;

    public void LoadStaringScene()
    {
        LoadScene(scenes[startSceneIndex]);
    }

    public void LoadScene(string sceneName)
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
        catch (System.Exception)
        {
            Debug.Log("Scene can't be loaded");
            return;
        }
        
        currentScene = sceneName;
    }
}