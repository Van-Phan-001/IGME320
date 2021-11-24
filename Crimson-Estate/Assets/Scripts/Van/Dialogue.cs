using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    //----------------------------Fields---------------------------
    [Header("Serialized attributes")]
    [SerializeField] private Text text; //The text component where the current sentence should be displayed
    [SerializeField] private Image image; //The image component for what/who is currently talking
    [SerializeField] private string sentence = "Default sentence"; //current sentence to be written out
    [SerializeField] private float textDelaySeconds = .1f;
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private List<string> spritesIDs;
    
    [SerializeField] private Sprite defaultImage;
    //-------------------------------------------------------------

    private List<string> responses; //the responses grabbed from the current 
    private int sentenceIndex = 0; //which sentence should be written onto the writer 
    private bool writing = false; //used to prevent user from spamming sentence writer
    private Dictionary<string, Sprite> objImages; //images that we can switch to



    private void Start()
    {
        //if (text != null)
        //{
        //    text.text = string.Empty;
        //    StartDialogue();
        //}
        responses = new List<string>() { "Just a couple sentences for testing. I made this sentence extra long to see how words wrap for my box.",
                                        $"I am the sentence index: {sentenceIndex}",
                                         "We are at the end!!!" };

        if(objImages == null) objImages = new Dictionary<string, Sprite>();
        AssignSpritesWithIDs();
    }

    /// <summary>
    /// Loops through the IDs and Sprites and assigns them into our dictionary
    /// </summary>
    private void AssignSpritesWithIDs()
    {
        for (int i = 0; i < sprites.Count; i++)
        {
            objImages.Add(spritesIDs[i], sprites[i]);
        }
    }

    /// <summary>
    /// Assigns the requested image to our profile box if that image exists
    /// </summary>
    public void AssignImage(string a_sImageName)
    {
        if (objImages.ContainsKey(a_sImageName)) image.sprite = objImages[a_sImageName]; //if our dictionary has the requested image, assign it into the profile pic
        else image.sprite = defaultImage;
    }

    /// <summary>
    /// Changes responses to our new responses list
    /// </summary>
    public void AssignNewResponse(List<string> a_lReponses)
    {
        sentenceIndex = 0;
        responses = new List<string>(a_lReponses);
    }

    /// <summary>
    /// Writes out the next sentence from our current responses list
    /// </summary>
    public void PrintSentence()
    {
        if (writing == false)
        {
            text.text = ""; //Clear text before printing out sentence
            StartDialogue(responses[sentenceIndex]);
            sentenceIndex++;
            sentenceIndex = Mathf.Clamp(sentenceIndex, 0, responses.Count - 1);
        }
    }

    /// <summary>
    /// Starts the typing out the passed in sentence
    /// </summary>
    private void StartDialogue(string a_sSentence)
    {
        sentence = a_sSentence;
        StartCoroutine(TypeLine());
    }

    /// <summary>
    /// Coroutined sentence typer
    /// </summary>
    IEnumerator TypeLine()
    {
        writing = true;
        foreach (char c in sentence)
        {
            text.text += c;
            yield return new WaitForSeconds(textDelaySeconds);
        }
        writing = false;
    }
}
