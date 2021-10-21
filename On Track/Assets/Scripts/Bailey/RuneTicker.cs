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
    int slotId;

    [SerializeField]
    GameManager gm;
    [SerializeField]
    int currentValue;

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
                break;
            case 2:
                displayText.text = "II";
                break;
            case 3:
                displayText.text = "III";
                break;
            case 4:
                displayText.text = "IV";
                break;
            case 5:
                displayText.text = "V";
                break;
            case 6:
                displayText.text = "VI";
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
