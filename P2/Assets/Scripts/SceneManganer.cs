using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManganer : MonoBehaviour
{
    public List<string> sceneName = new List<string>();
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

    public void loadScene(int sceneId) {
        SceneManager.LoadScene(sceneName[sceneId], LoadSceneMode.Single);
    }

    public void loadScene(string sceneName)
    {

    }
}
