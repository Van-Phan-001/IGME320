using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commands : MonoBehaviour
{
    private Dictionary<int, string[]> commands;
    int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        commands = new Dictionary<int, string[]>();
    }

    public void AssignCommands(Dictionary<int, string[]> a_commands)
    {
        commands = a_commands;
        index = 0;
    }

    /// <summary>
    /// Runs the next command in our dictionary
    /// </summary>
    public void NextCommand()
    {
        try
        {
            string command = commands[index][0]; //The first string in our string index is a command
            switch (command)
            {
                case "GiveIdea":
                    Debug.Log("Giving idea: ");
                    break;

                default:
                    Debug.Log($"Command: {command}");
                    break;
            }
            index++;
            index = Mathf.Clamp(index, 0, commands.Count - 1);
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
