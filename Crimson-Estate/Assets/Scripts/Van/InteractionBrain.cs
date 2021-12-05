using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The inventory the contains all the abilities of the player
/// </summary>
public class InteractionBrain : MonoBehaviour
{
    [SerializeField] private GameObject brainCanvas;
    [SerializeField] private GameObject dialogue;
    private bool activeState = false;
    private bool activeStateDialogue = true;


    #region Singleton definition
    private static InteractionBrain _instance;
    public static InteractionBrain Instance { get { return _instance; } }
    //Code to reference singleton: SceneChanger.Instance;
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        brainCanvas.SetActive(activeState);
    }

    /// <summary>
    /// Changes brain from active and inactive
    /// </summary>
    public void SwitchBrainState()
    {
        activeStateDialogue = !activeStateDialogue;
        activeState = !activeState;
        brainCanvas.SetActive(activeState);
        dialogue.SetActive(activeStateDialogue);

        /*if (activeState)
        {
            brainCanvas.GetComponent<RectTransform>().localPosition = new Vector3(10000f, 0f, 0f);
        }
        if (activeStateDialogue)
        {
            brainCanvas.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 0f);
        }
        */
        

        //determines whether mouse free floats for inventory or locked in middle for player controls
        if (Cursor.lockState == CursorLockMode.Locked) Cursor.lockState = CursorLockMode.Confined;
        else if (Cursor.lockState == CursorLockMode.Confined) Cursor.lockState = CursorLockMode.Locked;

        Debug.Log($"Dialogue: {activeStateDialogue} Ideas: {activeState}");
    }
}
