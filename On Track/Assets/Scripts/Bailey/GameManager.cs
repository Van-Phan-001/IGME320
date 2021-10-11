using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{ // GameManager class manages points of player interactivity
  // and creates hints based on inputs
  // fields
    int startingGold;
    int currentGold;
    [SerializeField]
    int numberOfSlots;
    [SerializeField]
    bool sumHint;

    List<int> answerKey;
    [SerializeField]
    List<int> currentGuess;
    // Start is called before the first frame update
    void Start()
    {
        // print guess
        string currentGuessLog = "Current Guess is " + GuessString() + "and you can change it in the inspector for now!";
        Debug.Log(currentGuessLog);

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

        Debug.Log(answerResult);

        // set game variables based on PlayerPrefs
        
        // DEBUG
        //Theurgist Hint
        Debug.Log(TheurgistHint());

        //Sorceror Hint
        Debug.Log(SorcererHint());

        //Wizard Hint
        Debug.Log(WizardHint());

        //Witch Hint
        Debug.Log(WitchHint());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // Methods
    // this method will return the string version of the currently inputted guess. 
    string GuessString()
    {
        string guessResult = "";
        foreach (var item in currentGuess)
        {
            guessResult += item.ToString() + " ";
        }
        // note should remove final space at the end
        return guessResult;
    }
    // NOTE: costs for getting hints are processed at button press, and should deny invoking
    // these methods if there isn't enough gold.

    // Tells you the exact positions that have the correct number.
    string TheurgistHint()
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
    string SorcererHint()
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
    string WizardHint()
    {
        // grab random position
        int randomPosition = Random.Range(1, numberOfSlots + 1);
        // give the player the hint based on generated position
        string result = "Ahhh... I see... Slot " + randomPosition + "'s correct rune power is " + answerKey[randomPosition - 1] + "!";
        return result;
    }
    // Tells you the number of positions that have too high and too low of a number.
    string WitchHint()
    {
        // start 2 new integers to count number of too high and too low
        string result;
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
            return "You have 0 slots with incorrect rune power! Use Demonologist to lock in your answer!";
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
}
