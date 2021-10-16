using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    [Header("Tutorial pages")]
    [SerializeField] private List<Sprite> tutorialPages;
    [SerializeField] private Image currentPage; //address for background image that displays current page

    private int currentPageIndex = 0;

    private void Start()
    {
        ChangeScene();
    }

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
    }


}
