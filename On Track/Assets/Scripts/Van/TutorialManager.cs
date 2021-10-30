using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    #region Fields
    [Header("Tutorial pages")]
    [SerializeField] private List<Sprite> tutorialPages;
    [SerializeField] private Image currentPage; //address for background image that displays current page
    [SerializeField] private Text currentText; //address of current text in dialog
    private int currentPageIndex = 0;
    #endregion

    #region Start
    private void Start()
    {
        ChangeScene();
    }
    #endregion

    #region Functions
    public void NextPage()
    {
        currentPageIndex++;
        if (currentPageIndex == tutorialPages.Count) SceneManager.LoadScene("Game");
        currentPageIndex = PageIndexClamp(currentPageIndex);
        ChangeScene();
    }

    public void PrevPage()
    {
        currentPageIndex--;
        currentPageIndex = PageIndexClamp(currentPageIndex);
        ChangeScene();
    }

    private int PageIndexClamp(int _index)
    {
        return Mathf.Clamp(_index, 0, tutorialPages.Count - 1);
    }

    private void ChangeScene()
    {
        currentPage.sprite = tutorialPages[currentPageIndex];
        switch (currentPageIndex)
        {
            case 0:
                currentText.text = "The demonologist, Michael, and his imp helper are looking for Loredon, a Demon who knows all the secrets for Ethshar.";
                break;
            case 1:
                currentText.text = "They find his inscription on the wall of the cave, and begin to summon him.";
                break;
            case 2:
                currentText.text = $"``FOOLS`` Loredon boomed, ``YOU CANNOT SIMPLY SUMMON ME AND EXPECT ME TO TELL YOU THINGS WITHOUT SOME REPAYMENT.``";
                break;
            case 3:
                currentText.text = "HOW ABOUT A GAME, YES? TAKE THESE RUNES AND SOLVE MY PUZZLE. YOU CAN SOLVE IT BY ANY MEANS NESCESSARY.";
                break; 
            default:
                currentText.text = "This should never get here??";
                break;
        }
    }
    #endregion
}
