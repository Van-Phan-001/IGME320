using UnityEngine;
using UnityEngine.UI;

public class DragIn : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private RaycastManager raycastManager;
    [SerializeField] private Text showInHand; //The text that shows what action the user currently has equipped
    private AudioManager am;

    public void Start()
    {
        am = AudioManager.Instance;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.name != "OnHover")
        {
            am.PlaySound("HandInto");
            string action = collision.name; //this just says default
            raycastManager.currentAction = action; //This becomes two quotes
            text.text = action; //this says default
            showInHand.text = action; //this just says default
        }
    }

    public void SetDefault()
    {
        am.PlaySound("HandClear");
        raycastManager.currentAction = ""; //This becomes two quotes
        text.text = "Default"; //this says default
        showInHand.text = "Default"; //this just says default
    }

}
