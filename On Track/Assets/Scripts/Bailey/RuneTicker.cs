using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuneTicker : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Button upTicker;
    [SerializeField]
    Button downTicker;
    [SerializeField]
    Text displayText;
    [SerializeField]
    Image displayImage;

    [SerializeField]
    int slotId;

    [SerializeField]
    GameManager gm;
    [SerializeField]
    int currentValue;

    [SerializeField]
    Sprite rune1;
    [SerializeField]
    Sprite rune2;
    [SerializeField]
    Sprite rune3;
    [SerializeField]
    Sprite rune4;
    [SerializeField]
    Sprite rune5;
    [SerializeField]
    Sprite rune6;

    private void Start()
    {
        SetDisplay();
    }

    void SetDisplay()
    {
        switch (currentValue)
        {
            case 1: 
                displayText.text = "I";
                displayImage.sprite = rune1;
                break;
            case 2:
                displayText.text = "II";
                displayImage.sprite = rune2;
                break;
            case 3:
                displayText.text = "III";
                displayImage.sprite = rune3;
                break;
            case 4:
                displayText.text = "IV";
                displayImage.sprite = rune4;
                break;
            case 5:
                displayText.text = "V";
                displayImage.sprite = rune5;
                break;
            case 6:
                displayText.text = "VI";
                displayImage.sprite = rune6;
                break;
            default: break;
        }
    }
    public void UpTickPress()
    {
        if (currentValue == 6)
        {
            currentValue = 1;
        }
        else currentValue++;

        SetDisplay();
        gm.GuessChange(slotId, currentValue);
    }
    public void DownTickPress()
    {
        if (currentValue == 1)
        {
            currentValue = 6;
        }
        else currentValue--;

        SetDisplay();
        gm.GuessChange(slotId, currentValue);
    }
}
