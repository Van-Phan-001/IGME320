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

    private void Start()
    {
        //0 represents if we have not been to scene
        //PlayerPrefs.GetFloat("playerTutorial"); //line of code to adjust player pref values 
    }
    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    //base load game with player pref logic (can be used for custom launch)
    public void ToGame()
    {
            SceneManager.LoadScene("Game");
    }
    //add player pref numbers 
    public void ToGameEasy()
    {
        SceneManager.LoadScene("Game");
    }
    public void ToGameMed()
    {
        SceneManager.LoadScene("Game");
    }
    public void ToGameHard()
    {
        SceneManager.LoadScene("Game");
    }
    public void ToTutorialScene()
    {
        SceneManager.LoadScene("TutorialScene");
    }
    public void ToWinState()
    {
        SceneManager.LoadScene("WinState");
    }
    public void ToLoseState()
    {
        SceneManager.LoadScene("LoseState");
    }
    public void ToSelectTypeScene()
    {
        SceneManager.LoadScene("SelectionTypeScene");
    }
    public void ToRegularSelection()
    {
        SceneManager.LoadScene("RegularSelection");
    }
    public void ToCustomSelect()
    {
        SceneManager.LoadScene("CustomSelect");
    }
}
