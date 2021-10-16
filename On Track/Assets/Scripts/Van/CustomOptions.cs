using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CustomOptions : MonoBehaviour
{
    [SerializeField] private Text goldText;

    private int gold = 100;
    private int numSlots = 5;
    private int sum = 0;

    private void Start()
    {
        //defaults
        PlayerPrefs.SetInt("gold", gold);
        PlayerPrefs.SetInt("numSlots", numSlots);
        PlayerPrefs.SetInt("sum", sum); //sum 0 == yes

        goldText.text = $"{gold}G";
    }
    public void goldPlusTen()
    {
        gold += 10;
        goldText.text = $"{gold}G";
    }
    public void goldMinusTen()
    {
        gold -= 10;
        goldText.text = $"{gold}G";
    }
    public void goldPlusHundred()
    {
        gold += 100;
        goldText.text = $"{gold}G";
    }
    public void goldMinusHundred()
    {
        gold -= 100;
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
    public void setSumYes()
    {
        sum = 1;
    }
    public void setSumNo()
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
        SceneManager.LoadScene("Game");
    }
}
