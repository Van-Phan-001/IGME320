using UnityEngine;
using UnityEngine.UI;

public class DragIn : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private RaycastManager raycastManager;
    [SerializeField] private Text showInHand; //The text that shows what action the user currently has equipped

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string action = collision.name;
        raycastManager.currentAction = action;
        text.text = action;
        showInHand.text = action;
    }

}
