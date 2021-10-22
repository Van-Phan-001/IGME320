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
    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    //base load game with player pref logic (can be used for custom launch)
    public void ToGame()
    {
        // Later: use difficulties to change player prefs and then call this method
        // use switch on slot number to handle which scene is started.
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
    public void ToExit()
    {
        //this only runs in built version
        Application.Quit();
    }
    #endregion

}
