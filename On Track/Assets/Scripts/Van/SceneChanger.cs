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
        switch (PlayerPrefs.GetInt("numSlots"))
        {
            case 4:
            default:
                SceneManager.LoadScene("Game");
                break;
            case 5:
                SceneManager.LoadScene("Game5Slots");
                break;
            case 6:
                SceneManager.LoadScene("Game6Slots");
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
