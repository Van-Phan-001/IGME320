using UnityEngine;
using UnityEngine.UIElements;

public class InteractionBrain : MonoBehaviour
{
    [SerializeField] private GameObject brainCanvas;
    private bool activeState = false;

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
        brainCanvas.SetActive(activeState);
    }

    /// <summary>
    /// Changes brain from active and inactive
    /// </summary>
    public void SwitchBrainState()
    {
        activeState = !activeState;
        brainCanvas.SetActive(activeState);
    }
}
