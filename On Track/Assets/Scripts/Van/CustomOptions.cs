using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CustomOptions : MonoBehaviour
{
    #region Fields
    [SerializeField] private Text goldText;
    private int gold = 100;
    private int numSlots = 5;
    private int sum = 0;
    private int goldMax = 1000;
    private int goldMin = 0;
    #endregion

    #region Start
    private void Start()
    {
        //defaults
        PlayerPrefs.SetInt("gold", gold);
        PlayerPrefs.SetInt("numSlots", numSlots);
        PlayerPrefs.SetInt("sum", sum); //sum 0 == yes

        goldText.text = $"{gold}G";
    }
    #endregion

    #region Functions
    private int GoldClamp(int gold)
    {
        return Mathf.Clamp(gold, goldMin, goldMax);
    }
    public void goldPlusTen()
    {
        gold += 10;
        gold = GoldClamp(gold);
        goldText.text = $"{gold}G";
    }
    public void goldMinusTen()
    {
        gold -= 10;
        gold = GoldClamp(gold);
        goldText.text = $"{gold}G";
    }
    public void goldPlusHundred()
    {
        gold += 100;
        gold = GoldClamp(gold);
        goldText.text = $"{gold}G";
    }
    public void goldMinusHundred()
    {
        gold -= 100;
        gold = GoldClamp(gold);
        goldText.text = $"{gold}G";
    }
    public void setSlotsFour()
    {
        numSlots = 4;
    }
    public void setSlotsFive()
    {
        numSlots = 5;
    }
    public void setSlotsSix()
    {
        numSlots = 6;
    }
    public void setSumNo()
    {
        sum = 1;
    }
    public void setSumYes()
    {
        sum = 0;
    }

    //save all values to prefs and launch game
    public void toGame()
    {
        PlayerPrefs.SetInt("gold", gold);
        PlayerPrefs.SetInt("numSlots", numSlots);
        PlayerPrefs.SetInt("sum", sum); //sum 0 == yes
        Debug.Log($"Gold: {PlayerPrefs.GetInt("gold")}, Slots: {PlayerPrefs.GetInt("numSlots")}, Sum: {PlayerPrefs.GetInt("sum")}");
        SceneChanger sc = FindObjectOfType<SceneChanger>();
        sc.ToGame();
    }
    #endregion
}
