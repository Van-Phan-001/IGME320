using UnityEngine;

/// <summary>
/// Contains all references to objects that need to interact when raycasting for player
/// </summary>
public class RaycastManager : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue; //The dialogue box that changes text/images
    [SerializeField] private Commands commands; //The script that contains all command scenarios
    [SerializeField] private DragIn dragIn; //The obj the changes text based off what idea is dragged over it

    public string currentAction = "";

    public Dialogue Dialogue { get { return dialogue; } }
    public Commands Commands { get { return commands; } }
}
