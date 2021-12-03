using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commands : MonoBehaviour
{
    private IdeaManager id;
    private Dictionary<int, string[]> commands;
    int index = 0;
    private bool commandsDone = false; 
    // Start is called before the first frame update
    void Start()
    {
        commands = new Dictionary<int, string[]>();
    }

    public void AssignCommands(Dictionary<int, string[]> a_commands)
    {
        commandsDone = false;
        commands = a_commands;
        index = 0;
    }

    /// <summary>
    /// Runs the next command in our dictionary
    /// </summary>
    public void NextCommand()
    {
        id = FindObjectOfType<IdeaManager>();
        try
        {
            if (commandsDone) return;
            string command = commands[index][0]; //The first string in our string index is a command
            switch (command)
            {
                case "GiveIdea": //Adds this idea to the mind palace
                    id.CreateIdea(commands[index][1]);
                    Debug.Log($"Giving idea: {commands[index][1]}");
                    break;
                case "UpdateIdea": //Updates the respective idea
                    id.UpdateIdea(commands[index][1]);
                    Debug.Log($"Updating idea: {commands[index][1]}");
                    break;
                case "SwitchTo": //Switches the image in dialogue to whoever is currently speaking
                    Debug.Log($"Switch to:  {commands[index][1]}");
                    break;
                case "Introduced": //Sets introduced to true for the obj we're talking to
                    DialogueReader dr = FindObjectOfType<PlayerController>().objDialogue;
                    dr.hasIntroduced = true;
                    Debug.Log("Current Dialogue has introduced themself!");
                    break;
                case "Suggest": //Displays a suggestion on the top right of screen
                    Debug.Log($"Suggesting: {commands[index][1]}");
                    break;
                case "OpenDoor": //Open door animation plus go into next scene after a while
                    Debug.Log("Opening door");
                    break;
                default: //Runs filler text dialogue if there is any
                    Debug.Log($"Command: {commands[index][0]}");
                    break;
            }
            index++;
            index = Mathf.Clamp(index, 0, commands.Count - 1);
            if(index == commands.Count - 1)
            {
                commandsDone = true;
            }
        }
        catch
        {
            Debug.Log("Commands error");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
