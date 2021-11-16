using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DialogueReader : MonoBehaviour
{
    [SerializeField]
    public Dictionary<string, List<string>> responses;
    [SerializeField]
    public Dictionary<string, Dictionary<int, string[]>> commandDictionary;
    Dictionary<int, string[]> commands;
    [SerializeField]
    string fileName;

    enum Stages
    {
        Opening,
        SentanceFinding
    }
    // Start is called before the first frame update
    void Start()
    {
        fileName = "Assets/Dialogue/" + fileName + ".txt";
        responses = new Dictionary<string, List<string>>();
        commands = new Dictionary<int, string[]>();
        commandDictionary = new Dictionary<string, Dictionary<int, string[]>>();
        string tempKey = "";
        List<string> tempMessages = new List<string>();
        int messageIndex = 0;
        Stages currentPhase = Stages.Opening;
        if (File.Exists(fileName))
        {
            foreach (string readLine in System.IO.File.ReadLines(fileName))
            {
                string currentLine = readLine;
                switch (currentPhase)
                {
                    case Stages.Opening:
                        if (currentLine.EndsWith("{"))
                        {
                            // set our temp key and start finding sentances
                            currentLine = currentLine.Replace("{", " ");
                            currentLine = currentLine.Trim().ToLower();
                            tempKey = currentLine;
                            tempMessages = new List<string>();
                            messageIndex = -1;
                            currentPhase = Stages.SentanceFinding;
                        }
                        break;
                    case Stages.SentanceFinding:
                        // check for last 
                        if (currentLine.Contains("}"))
                        {
                            // finalize current dictionary
                            responses.Add(tempKey, tempMessages);

                            // add current command dictionary to command dictionary dictionary
                            commandDictionary.Add(tempKey, commands);

                            // reset command dictionary
                            commands = new Dictionary<int, string[]>();

                            // start looking for new response
                            currentPhase = Stages.Opening;
                            break;
                        }
                        // check for command
                        if (!currentLine.Contains("/"))
                        {
                            // if not, add current string as dialogue
                            tempMessages.Add(currentLine.Trim());
                        }
                        else
                        {
                            // split command and argument and save it to command dictionary
                            string command = currentLine.Trim();
                            string[] tempCommandList = command.Split(' ');
                            tempCommandList[0] = tempCommandList[0].Replace("/", "").Trim();

                            if (tempCommandList.Length == 1)
                            {
                                
                                commands.Add(messageIndex, new string[2] { tempCommandList[0], "" });
                            }
                            else
                            {
                                tempCommandList[1] = tempCommandList[1].Trim();
                                commands.Add(messageIndex, new string[2] { tempCommandList[0], tempCommandList[1] });
                            }
                            messageIndex--;
                        }
                        break;
                }
                messageIndex++;
            }
        }
        else
        {
            // Create a new file at given location    
            FileStream fs = File.Create(fileName);
        }

    }
}