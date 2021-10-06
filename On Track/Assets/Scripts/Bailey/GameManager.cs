using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{ // GameManager class manages points of player interactivity
  // and creates hints based on inputs
  // fields
    int startingGold;
    int currentGold;
    [SerializeField]
    int slots;
    [SerializeField]
    bool sumHint;

    List<int> answerKey;
    [SerializeField]
    List<int> currentGuess;
    // Start is called before the first frame update
    void Start()
    {
        // create answer based on number of slots
        answerKey = new List<int>();
        for (int i = 0; i < slots; i++)
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

        // set variables based on PlayerPrefs

        // DEBUG
        //Theurgist Hint
        Debug.Log(TheurgistHint());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // Methods
    // NOTE: costs for getting hints are processed at button press, and should deny invoking
    // these methods if there isn't enough gold.
    string TheurgistHint()
    {
        List<bool> isCorrectList = new List<bool>();
        string result;
        bool allCorrect = true;
        // gather boolean list
        for (int i = 0; i < slots; i++)
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
        for (int i = 0; i < slots; i++)
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
}
