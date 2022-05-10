using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManganer : MonoBehaviour
{
    public static GameManganer Instance;
    public GameObject gameManagerPrefab;
    public SceneManganer sceneManganer;
    public int sceneOfDeath;
    public bool swipe = false;
    public PlayerMovement player;
    [SerializeField]
    bool disableUAP = false;
    public UAP_AccessibilityManager accessibilityManager;
    public int deathCount;
    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void Death()
    {
        sceneOfDeath = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene("GameOver");
        Debug.Log("Get Owned Loser");
        deathCount++;
    }

    void disableIfNotMenu() {
        
        accessibilityManager = FindObjectOfType<UAP_AccessibilityManager>();
        
        if (accessibilityManager != null && SceneManager.GetActiveScene().name != "GameOver" && SceneManager.GetActiveScene().buildIndex != 0)
        {
            Debug.Log("AccessibilityManager: " + accessibilityManager + " Object: " + accessibilityManager.gameObject.name + " parent: " + accessibilityManager.gameObject.transform.parent.name);
            accessibilityManager.enabled = false;
        }
    }
    private void Update()
    {
        if (player == null && SceneManager.GetActiveScene().name != "GameOver" && SceneManager.GetActiveScene().buildIndex != 0)
        {
            player = FindObjectOfType<PlayerMovement>();
        }

        if (accessibilityManager == null)
        {
            disableIfNotMenu();
        }
        else if (SceneManager.GetActiveScene().name != "GameOver" && SceneManager.GetActiveScene().buildIndex != 0 && !disableUAP) {
            Debug.Log("disable UAP");
            disableUAPObj();
        } else if ((SceneManager.GetActiveScene().name == "GameOver" || SceneManager.GetActiveScene().buildIndex == 0 )&& disableUAP) {
            Debug.Log("aktivate UAP");
            aktivateUAPObj();
        }
    }

    private void aktivateUAPObj()
    {
        if (accessibilityManager != null )
        {
            disableUAP = false;
            accessibilityManager.m_HandleMagicGestures = false;
        }
    }

    private void disableUAPObj()
    {
        if (accessibilityManager != null)
        {
            disableUAP = true;
            accessibilityManager.m_HandleMagicGestures = true;
        }
    }

    public void winGame() {
        Debug.Log("game won ");
        if (sceneManganer == null) { 
            sceneManganer = FindObjectOfType<SceneManganer>();
        }
        if (sceneManganer != null) {
            sceneManganer.loadNextLevel();
            }
        else{
                Debug.LogWarning("et eller andet er galt");
            }
        
    }

}
