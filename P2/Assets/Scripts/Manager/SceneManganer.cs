using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManganer : MonoBehaviour

{
    public GameObject loadingScreen;
    public Slider slider;

    public void LoadLevel(string sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously(string sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;

            yield return null;
        }
    }
    public List<string> sceneName = new List<string>();

    internal static void LoadSceneAsync(int sceneIndex)
    {
        throw new NotImplementedException();
    }

    public int levelIndex = 0;
    public static SceneManganer instance;
    public float plus = 1;
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
        if (levelIndex + plus< sceneName.Count)
        {
            Debug.Log(levelIndex);
            levelIndex += 1;
            SceneManager.LoadScene(sceneName[levelIndex], LoadSceneMode.Single);
        }
        else {
            Debug.LogWarning("no more level");
            SceneManager.LoadScene("MainMenu");
            levelIndex = 0;
        }
        
    }
    public void loadScene(int sceneId) {
        SceneManager.LoadScene(sceneName[sceneId], LoadSceneMode.Single);
    }

    public void loadScene(string sceneName)
    {
        StartCoroutine(LoadAsynchronously(sceneName));
    }


}
