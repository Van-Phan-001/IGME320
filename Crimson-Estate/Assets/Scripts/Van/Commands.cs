using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commands : MonoBehaviour
{
    private IdeaManager id;
    private Dictionary<int, string[]> commands;
    [SerializeField] private Dialogue dialogue;
    [SerializeField] private Animator gateAnimator;

    private SceneChanger sceneChanger;
    private AudioManager audioManager;
    InputManager ip;

    int index = 0;
    private bool commandsDone = false;

    // Start is called before the first frame update
    public void Update()
    {
        if (ip.PlayerJumped())
        {
            GiveAccusation();
        }
    }
    void Start()
    {
        ip = InputManager.Instance;
        id = FindObjectOfType<IdeaManager>();
        sceneChanger = SceneChanger.Instance;
        audioManager = AudioManager.Instance;
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
                    Debug.Log($"Giving idea: {commands[index][1]}");
                    if (!id.CreatedIdeas.ContainsKey("{commands[index][1]"))
                    {
                        dialogue.Suggest("You just learned the idea: " + commands[index][1]);
                    }
                    id.CreateIdea(commands[index][1]);
                    break;
                case "UpdateIdea": //Updates the respective idea
                    id.UpdateIdea(commands[index][1]);
                    Debug.Log($"Updating idea: {commands[index][1]}");
                    break;
                case "SwitchTo": //Switches the image in dialogue to whoever is currently speaking
                    Debug.Log($"Switch to:  {commands[index][1]}");
                    dialogue.AssignImage(commands[index][1]);
                    break;
                case "Introduced": //Sets introduced to true for the obj we're talking to
                    DialogueReader dr = FindObjectOfType<PlayerController>().objDialogue;
                    dr.hasIntroduced = true;
                    Debug.Log("Current Dialogue has introduced themself!");
                    break;
                case "Suggest": //Displays a suggestion on the top right of screen
                    Debug.Log($"Suggesting: {commands[index][1]}");
                    string suggestion = commands[index][1].Replace("_", " ");
                    dialogue.Suggest(suggestion);
                    break;
                case "OpenDoor": //Open door animation plus go into next scene after a while
                    Debug.Log("Opening door");
                    OpenGates();
                    break;
                default: //Runs filler text dialogue if there is any
                    Debug.Log($"Command: {commands[index][0]}");
                    break;
            }
            index++;
            if(index == commands.Count)
            {
                commandsDone = true;
            }
        }
        catch
        {
            Debug.Log("No command");
            index++;
        }
        
    }

    private void OpenGates()
    {
        StartCoroutine(GateAnimateSceneLoad());
    }

    IEnumerator GateAnimateSceneLoad()
    {
        gateAnimator.SetTrigger("OpenGate");
        audioManager.PlaySound("OpenGate");
        yield return new WaitForSeconds(5.0f);
        sceneChanger.ToScene(.5f,"GavinTesting");
    }

    public void GiveAccusation()
    {
        id.CreateIdea("crimsonAccusation");
        dialogue.Suggest("You've gathered enough information... finish the job and accuse a suspect using the Crimson Accusation!");
    }


}


