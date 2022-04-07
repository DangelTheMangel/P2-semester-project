using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManganer : MonoBehaviour
{
    public List<string> sceneName = new List<string>();
    public int levelIndex = 0;
    public static SceneManganer instance;

    public bool changeScene = false;
    public void Awake()
    {
        //tjekker om der er en instance og hvis der ikke er
        //gøre denne til instance ellers ødlæg dette gameobject
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        
    }

    public void loadNextLevel() {
        if (levelIndex + 1< sceneName.Count)
        {
            Debug.Log(levelIndex);
            levelIndex += 1;
            SceneManager.LoadScene(sceneName[levelIndex], LoadSceneMode.Single);
        }
        else {
            Debug.LogWarning("no more level");
        }
        
    }
    public void loadScene(int sceneId) {
        SceneManager.LoadScene(sceneName[sceneId], LoadSceneMode.Single);
    }

    public void loadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
