using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Attach events to each function based off scene the button
/// need to go to
/// </summary>
public class SceneChanger : MonoBehaviour
{
    #region Fields
    [Header("Testing values")]
    [SerializeField] private bool playTutorial;
    private float defaultDelay = .3f;

    #region Singleton definition
    public static SceneChanger instance;
    //Code to reference singleton: SceneChanger.instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion
    #endregion

    #region Functions
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            ToMainMenu();
        }
    }

    IEnumerator TransitionDelay(float a_fDelay = 5.0f,string sceneIndex = "MainMenu")
    {
        yield return new WaitForSeconds(a_fDelay);
        SceneManager.LoadScene(sceneIndex);
    }
    public void ToMainMenu()
    {
        StartCoroutine(TransitionDelay(.5f,"MainMenu"));
    }
    //base load game with player pref logic (can be used for custom launch)
    public void ToGame()
    {
        switch (PlayerPrefs.GetInt("numSlots"))
        {
            case 4:
            default:
                StartCoroutine(TransitionDelay(defaultDelay, "Game"));
                break;
            case 5:
                StartCoroutine(TransitionDelay(defaultDelay, "Game5Slots"));
                break;
            case 6:
                StartCoroutine(TransitionDelay(defaultDelay, "Game6Slots"));
                break;
        }
    }
    //add player pref numbers 
    public void ToGameEasy()
    {
        PlayerPrefs.SetInt("gold", 100);
        PlayerPrefs.SetInt("numslots", 4);
        PlayerPrefs.SetInt("sum", 0);
        ToGame();
    }
    public void ToGameMed()
    {
        PlayerPrefs.SetInt("gold", 100);
        PlayerPrefs.SetInt("numslots", 4);
        PlayerPrefs.SetInt("sum", 1);
        ToGame();
    }
    public void ToGameHard()
    {
        PlayerPrefs.SetInt("gold", 100);
        PlayerPrefs.SetInt("numslots", 5);
        PlayerPrefs.SetInt("sum", 1);
        ToGame();
    }
    public void ToTutorialScene()
    {
        StartCoroutine(TransitionDelay(defaultDelay, "TutorialScene"));
    }
    public void ToWinState()
    {
        StartCoroutine(TransitionDelay(defaultDelay, "WinState"));
    }
    public void ToLoseState()
    {
        StartCoroutine(TransitionDelay(defaultDelay, "LoseState"));
    }
    public void ToSelectTypeScene()
    {
        StartCoroutine(TransitionDelay(defaultDelay, "SelectionTypeScene"));
    }
    public void ToRegularSelection()
    {
        StartCoroutine(TransitionDelay(defaultDelay, "RegularSelection"));
    }
    public void ToCustomSelect()
    {
        StartCoroutine(TransitionDelay(defaultDelay, "CustomSelect"));
    }
    public void ToExit()
    {
        //this only runs in built version
        Application.Quit();
    }
    #endregion

}
