using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{ // GameManager class manages points of player interactivity
  // and creates hints based on inputs
  // fields
    [SerializeField]
    int startingGold;
    int currentGold;
    [SerializeField]
    int numberOfSlots;
    [SerializeField]
    bool sumHint;
    [SerializeField]
    GameObject messageBoxPrefab;
    [SerializeField]
    GameObject goldTextBox;

    string currentHintString;
    string prevHint;
    List<string> hintList;

    List<int> answerKey;
    [SerializeField]
    public List<int> currentGuess;

    #region Singleton definition
    public static GameManager instance;

    //Code to reference singleton: SceneChanger.instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    void Start()
    {
        hintList = new List<string>();


        startingGold = PlayerPrefs.GetInt("gold");
        currentGold = startingGold;

        // logging to help play
        currentHintString = "The demon has chosen " + numberOfSlots + " runes of power one through six in a specific order. " +
            "If you can guess them correctly utilizing your " + currentGold + " starting gold, you will be granted anything your " +
            "heart desires!";
        hintList.Add(currentHintString);
        PushMessage();

        // create answer based on number of slots
        answerKey = new List<int>();
        for (int i = 0; i < numberOfSlots; i++)
        {
            int tempRandom = Random.Range(1, 7); // random number 1 - 6
            answerKey.Add(tempRandom);
        }

        // log results
        string answerResult = "Answer key: ";
        foreach (var item in answerKey)
        {
            answerResult += item.ToString() + " ";
        }

        // give sum hint
        if (PlayerPrefs.GetInt("sum") == 0)
        {
            int totalSum = 0;
            foreach (var item in answerKey)
            {
                totalSum += item;
            }
            currentHintString = "The demon has opted to give you a starting hint: \nThe total sum of the rune powers is " + totalSum + ".";
            hintList.Add(currentHintString);
            PushMessage();
            StartCoroutine(ScrollUp());
        }


        Debug.Log(answerResult);
    }

    //TEMP UPDATE TO SHOW
    private void Update()
    {
        
    }

    IEnumerator ScrollUp()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject.FindObjectOfType<ScrollRect>().velocity = new Vector2(0f, -1000f);
    }
    IEnumerator ScrollDown()
    {
        yield return new WaitForSeconds(0.1f);
        GameObject.FindObjectOfType<ScrollRect>().velocity = new Vector2(0f, 1000f);
    }

    // Methods
    // this method will return the string version of the currently inputted guess. 
    string GuessString()
    {
        string guessResult = "";
        for (int i = 0; i < numberOfSlots; i++)
        {
            switch (currentGuess[i])
            {
                case 1:
                    guessResult += "I";
                    break;
                case 2:
                    guessResult += "II";
                    break;
                case 3:
                    guessResult += "III";
                    break;
                case 4:
                    guessResult += "IV";
                    break;
                case 5:
                    guessResult += "V";
                    break;
                case 6:
                    guessResult += "VI";
                    break;
            }
            if (i != numberOfSlots - 1)
            {
                guessResult += " ";
            }
        }

        return guessResult;
    }

    // this method prints the last thing in the current list to the game log and parents it to the scroll
    private void PushMessage()
    {
        GameObject message = Instantiate(messageBoxPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        Text newText = message.GetComponentInChildren<Text>();
        newText.text = hintList.LastOrDefault();
        message.transform.SetParent(GameObject.Find("ViewportContents").transform);
        message.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
        goldTextBox.GetComponent<Text>().text = "Current Gold: " + currentGold;
        StartCoroutine(ScrollDown());
    }

    #region Button Handlers

    public void GuessChange(int slotIndex, int changedNumber)
    {
        currentGuess[slotIndex] = changedNumber;
    }

    // Note: All these will have to create a new text box and add it to a scrolling textbox list
    // which can be done in another method
    private bool PriceHandler (string hintType)
    {
        switch (hintType)
        {
            case "theurgist":
                if (currentGold >= 25)
                {
                    currentGold -= 25;
                    return true;
                }
                break;
            case "sorceror":
                if (currentGold >= 10)
                {
                    currentGold -= 10;
                    return true;
                }
                break;
            case "wizard":
                if (currentGold >= 40)
                {
                    currentGold -= 40;
                    return true;
                }
                break;
            case "witch":
                if (currentGold >= 15)
                {
                    currentGold -= 15;
                    return true;
                }
                break;

            case "demonologist":
                return true;

            default:
                break;
        }
        return false;
    }

    public void TheurgistButton()
    {
        currentHintString = "";
        if (PriceHandler("theurgist"))
        {
            currentHintString = "Theurgist hint for guess \n" + GuessString() + ":\n" + TheurgistHint();
            hintList.Add(currentHintString);
        }
        else
        {
            currentHintString = "You don't have enough gold to use this hint! Current gold: " + currentGold;
        }
        hintList.Add(currentHintString);
        PushMessage();

    }
    public void SorcererButton()
    {
        currentHintString = "";
        if (PriceHandler("sorceror"))
        {
            currentHintString = "Sorcerer hint for guess \n" + GuessString() + ":\n" + SorcererHint();
        }
        else
        {
            currentHintString = "You don't have enough gold to use this hint! Current gold: " + currentGold;
        }
        hintList.Add(currentHintString);
        PushMessage();
    }
    public void WizardButton()
    {
        currentHintString = "";
        if (PriceHandler("wizard"))
        {
            currentHintString = "Wizard hint for guess \n" + GuessString() + ":\n" + WizardHint();
        }
        else
        {
            currentHintString = "You don't have enough gold to use this hint! Current gold: " + currentGold;
        }
        hintList.Add(currentHintString);
        PushMessage();
    }
    public void WitchButton()
    {
        currentHintString = "";
        if (PriceHandler("witch"))
        {
            currentHintString = "Witch hint for guess \n" + GuessString() + ":\n" + WitchHint();
        }
        else
        {
            currentHintString = "You don't have enough gold to use this hint! Current gold: " + currentGold;
        }
        hintList.Add(currentHintString);
        PushMessage();
    }
    public void DemonologistButton()
    {
        SceneChanger sc = FindObjectOfType<SceneChanger>();
        // bells and whistles can be added for effect
        List<bool> isCorrectList = new List<bool>();
        bool allCorrect = true;
        // gather boolean list
        for (int i = 0; i < numberOfSlots; i++)
        {
            if (answerKey[i] == currentGuess[i])
            {
                isCorrectList.Add(true);
            }
            else
            {
                isCorrectList.Add(false);
                allCorrect = false;
            }
        }
        // check for all correct
        if (allCorrect)
        {
            sc.ToWinState();
        }
        else sc.ToLoseState();
    }
    #endregion

    #region Hint String Creation
    // NOTE: costs for getting hints are processed at button press, and should deny invoking
    // these methods if there isn't enough gold.

    // Tells you the exact positions that have the correct number.
    private string TheurgistHint()
    {
        List<bool> isCorrectList = new List<bool>();
        string result;
        bool allCorrect = true;
        // gather boolean list
        for (int i = 0; i < numberOfSlots; i++)
        {
            if (answerKey[i] == currentGuess[i])
            {
                isCorrectList.Add(true);
            }
            else
            {
                isCorrectList.Add(false);
                allCorrect = false;
            }
        }
        // check for all correct
        if (allCorrect)
        {
            return "All slots are completely correct! Use Demonologist to lock in your answer!";
        }

        // create string hint based on correct results
        result = "The following slots contain the correct number/rune: ";
        for (int i = 0; i < numberOfSlots; i++)
        {
            if (isCorrectList[i] == true)
            {
                // POLISH NOTE: FIND WAY TO REMOVE COMMA FROM LAST ELEMENT
                result += "Slot " + (i + 1) + ", ";
            }
        }
        // if nothing is added to the result string, print completely new message
        if (result == "The following slots contain the correct number/rune: ")
        {
            return "Currently, you have no slots that contain the correct runes!";
        }
        return result;
    }
    // Tells you how many numbers are the correct number, but gives no information about position. 
    // Duplicate numbers are not counted again.
    private string SorcererHint()
    {
        string result = "";
        int numberOfCorrect = 0;
        List<int> uniqueCorrect = new List<int>();
        // gather number of correct
        for (int i = 0; i < numberOfSlots; i++)
        {
            for (int j = 0; j < numberOfSlots; j++) // nested to check every combination
            {
                if (answerKey[j] == currentGuess[i])
                {
                    // add to the integer list and check if unique.
                    uniqueCorrect.Add(currentGuess[i]);
                    // check for uniqueness
                    if (uniqueCorrect.Distinct().Count() == uniqueCorrect.Count()) // if new unique number, add to number of correct
                    {
                        numberOfCorrect++;
                    }
                    else // remove the number from the unique list and do not increment
                    {
                        uniqueCorrect.RemoveAt(uniqueCorrect.Count - 1);
                    }
                }
            }
        }

        // change result message based on correct slots
        
        if (numberOfCorrect == 0)
        {
            result = "Currently, none of your slots have a correct rune power.";
        }
        else if (numberOfCorrect == 1)
        {
            result = "You currently have 1 unique correct rune power.";
        }
        else
        {
            // note: Maybe this is how we should phrase it
            result = "You currently have " + numberOfCorrect + " unique correct rune powers.";
        }
        return result;
    }
    // Note: going over this, it doesn't use take the current guess into account, and is very random.
    // In all of our tests, no one chose this unironically. We should keep an eye on this and see if we should change it.
    // Tells you a random position correctly. It is possible to give you the same position on subsequent uses.
    private string WizardHint()
    {
        // grab random position
        int randomPosition = Random.Range(1, numberOfSlots + 1);
        // give the player the hint based on generated position
        string result = "Ahhh... I see... Slot " + randomPosition + "'s correct rune power is " + answerKey[randomPosition - 1] + "!";
        return result;
    }
    // Tells you the number of positions that have too high and too low of a number.
    private string WitchHint()
    {
        // start 2 new integers to count number of too high and too low
        string result = null;
        int numberTooHigh = 0;
        int numberTooLow = 0;
        for (int i = 0; i < numberOfSlots; i++)
        {
            int difference = currentGuess[i] - answerKey[i];

            if (difference > 0)
            {
                numberTooHigh++;
            }
            else if (difference < 0)
            {
                numberTooLow++;
            }
        }
        // check for different phrasing of response
        if (numberTooHigh == 0 && numberTooLow == 0)
        {
            // return "You have 0 slots with incorrect rune power! Use Demonologist to lock in your answer!";
        }
        else if (numberTooHigh == 0)
        {
            result = "You currently have " + numberTooLow + " slot(s) that have too low of a rune power.";
        }
        else if (numberTooLow == 0)
        {
            result = "You currently have " + numberTooHigh + " slot(s) that have too high of a rune power.";
        }
        else
        {
            result = "You currently have " + numberTooLow + " slot(s) with too low of a rune power AND " 
                + numberTooHigh + " slot(s) with too high of a rune power.";
        }
        return result;
    }
    #endregion
}
