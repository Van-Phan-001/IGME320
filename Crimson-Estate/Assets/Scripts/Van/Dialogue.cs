using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private string sentence = "Default sentence";
    [SerializeField] private float textDelaySeconds = .1f;

    private void Start()
    {
        if (text != null)
        {
            text.text = string.Empty;
            StartDialogue();
        }
    }
    void StartDialogue()
    {
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in sentence)
        {
            text.text += c;
            yield return new WaitForSeconds(textDelaySeconds);
        }
    }
}
