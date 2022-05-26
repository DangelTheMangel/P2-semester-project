using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public int deathCount = 0;
    [SerializeField]
    public double levelStartTime = 0;
    [SerializeField]
    public double levelEndTime = 0;
    public string nameUser = "testUser";
    public dataCollector dataCollector;
    public bool pausemenuOn = false;
    public InputField userName;
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

    private void Start()
    {
        dataCollector = gameObject.GetComponent<dataCollector>();
        userName.text = nameUser;
    }

    public void changeUserName() {
        if (userName != null) {
            nameUser = userName.text;
        }
        else {
            nameUser = "username";
        }
    }

    public void startTimer()
    {
        levelStartTime = Time.time;
    }
    public void Death()
    {
        //the death sound is played through the Soundmanager
        SoundManager.instance.playEffect(GameManganer.Instance.player.gameObject, "ArrowHit");
        //the current level that the player has reached is saved as the "sceneOfDeath" int, so that the player can return to the same level after a game over
        sceneOfDeath = SceneManager.GetActiveScene().buildIndex;
        //the game over scene is loaded
        SceneManager.LoadScene("GameOver");
        //the "deathcount" int is increased, this is for testing, to know how many times a test participant has lost.
        deathCount++;
    }

    void disableIfNotMenu() {
        
        accessibilityManager = FindObjectOfType<UAP_AccessibilityManager>();
        
        if (accessibilityManager != null && SceneManager.GetActiveScene().name != "GameOver" && SceneManager.GetActiveScene().buildIndex != 0)
        {
            accessibilityManager.enabled = false;
        }
    }
    private void Update()
    {
        //A "Playermovement" script is needed for the script to work when in a level. The game manager is not destroyed on load,
        //so changing scenes, like when getting a game over, will leave the gamemanager without a playermovement script.
        //This if-statement ensures that a playermovement script is searched for when it is missing.
        //It does not search when on the menu or game over screen 
        if (player == null && SceneManager.GetActiveScene().name != "GameOver" && SceneManager.GetActiveScene().buildIndex != 0)
        {
            player = FindObjectOfType<PlayerMovement>();
        }

        if (accessibilityManager == null)
        {
            disableIfNotMenu();
        }
        else if (SceneManager.GetActiveScene().name != "GameOver" && SceneManager.GetActiveScene().buildIndex != 0 && !disableUAP && !pausemenuOn) {
            Debug.Log("disable UAP");
            disableUAPObj();
        } else if ((SceneManager.GetActiveScene().name == "GameOver" || SceneManager.GetActiveScene().buildIndex == 0 )&& disableUAP) {
            Debug.Log("aktivate UAP");
            aktivateUAPObj();
        }
    }

    public void aktivateUAPObj()
    {
        if (accessibilityManager != null )
        {
            Debug.Log("#¤%");
            disableUAP = false;
            accessibilityManager.m_HandleUI = true;
        }
    }

    public void disableUAPObj()
    {
        if (accessibilityManager != null)
        {
            disableUAP = true;
            accessibilityManager.m_HandleUI = false;
        }
    }

    public void winGame() {
        Debug.Log("game won "); 
        levelEndTime = Time.realtimeSinceStartupAsDouble;
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
