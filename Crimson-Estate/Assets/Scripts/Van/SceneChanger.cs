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
    [Header("Scene values")]
    private const float m_fDefaultDelay = 1.0f;

    #region Singleton definition
    private static SceneChanger _instance;
    public static SceneChanger Instance { get { return _instance; } }
    //Code to reference singleton: SceneChanger.Instance;
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion
    #endregion

    #region Functions
    //private void Update()
    //{
    //    if (Input.GetKeyUp(KeyCode.Escape))
    //    {
    //        ToScene(1.0f,0);
    //    }
    //}

    /// <summary>
    /// Coroutine to transition to another scene with a slight delay
    /// </summary>
    IEnumerator TransitionDelay(float a_fDelay = m_fDefaultDelay, string a_sSceneIndex = "MainMenu")
    {
        yield return new WaitForSeconds(a_fDelay);
        SceneManager.LoadScene(a_sSceneIndex);
    }
    IEnumerator TransitionDelay(float a_fDelay = m_fDefaultDelay, int a_iSceneIndex = 0)
    {
        yield return new WaitForSeconds(a_fDelay);
        SceneManager.LoadScene(a_iSceneIndex);
    }

    /// <summary>
    /// Pass in the delay in seconds then pass in either the string or int index
    /// of the desired scene for the game to go to
    /// </summary>
    public void ToScene(float a_fDelay, string a_sSceneIndex = "MainMenu")
    {
        StartCoroutine(TransitionDelay(a_fDelay, a_sSceneIndex));
    }
    public void ToTutorial()
    {
        StartCoroutine(TransitionDelay(1.0f, "TutorialSceneNew"));
    }
    public void ToMenu()
    {
        StartCoroutine(TransitionDelay(1.0f, "MenuScene"));
    }

    public void ToScene(float a_fDelay = 5.0f, int a_iSceneIndex = 0)
    {
        StartCoroutine(TransitionDelay(a_fDelay, a_iSceneIndex));
    }

    /// <summary>
    /// Exits the game when in executable mode
    /// </summary>
    public void ToExit()
    {
        Application.Quit();
    }
    #endregion
}
