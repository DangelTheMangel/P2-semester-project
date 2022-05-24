using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Part of this script has been enspired by brakyes: https://www.youtube.com/watch?v=YMj2qPq9CP8

public class SceneManganer : MonoBehaviour

{
    public GameObject loadingScreen;
    public Slider slider;

    public List<string> sceneName = new List<string>();



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

    public void loadNextLevel() 
    {
        GameManganer.Instance.dataCollector.dataCollect();
        if (levelIndex + plus< sceneName.Count)
        {
            //UAP_AccessibilityManager.PauseAccessibility(true, false);
            //GameManganer.Instance.accessibilityManager.m_HandleUI = false;
            Debug.Log(levelIndex);
            levelIndex += 1;
            SceneManager.LoadScene(sceneName[levelIndex], LoadSceneMode.Single);
        }
        else {
            //UAP_AccessibilityManager.PauseAccessibility(false, false);
            //GameManganer.Instance.accessibilityManager.m_HandleUI = true;
            Debug.LogWarning("no more level");
            SceneManager.LoadScene("MainMenu");
            levelIndex = 0;
        }
        
    }
    public void loadScene(int sceneId) {
        SceneManager.LoadScene(sceneName[sceneId], LoadSceneMode.Single);
    }
    public void LoadLevel(string sceneIndex)
    {
        Debug.Log("level: " + sceneIndex);
        GameManganer.Instance.levelStartTime = Time.realtimeSinceStartupAsDouble;
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously(string sceneIndex)
    {
        Debug.Log("level: " + sceneIndex);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        if(loadingScreen != null)
        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            Debug.Log("progress: " + progress);
            if (slider != null)
                slider.value = progress;

            yield return null;
        }
    }


}
