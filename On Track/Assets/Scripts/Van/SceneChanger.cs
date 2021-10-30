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
                StartCoroutine(TransitionDelay(.5f, "Game"));
                break;
            case 5:
                StartCoroutine(TransitionDelay(.5f, "Game5Slots"));
                break;
            case 6:
                StartCoroutine(TransitionDelay(.5f, "Game6Slots"));
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
        StartCoroutine(TransitionDelay(.5f, "TutorialScene"));
    }
    public void ToWinState()
    {
        StartCoroutine(TransitionDelay(.5f, "WinState"));
    }
    public void ToLoseState()
    {
        StartCoroutine(TransitionDelay(.5f, "LoseState"));
    }
    public void ToSelectTypeScene()
    {
        StartCoroutine(TransitionDelay(.5f, "SelectionTypeScene"));
    }
    public void ToRegularSelection()
    {
        StartCoroutine(TransitionDelay(.5f, "RegularSelection"));
    }
    public void ToCustomSelect()
    {
        StartCoroutine(TransitionDelay(.5f, "CustomSelect"));
    }
    public void ToExit()
    {
        //this only runs in built version
        Application.Quit();
    }
    #endregion

}
