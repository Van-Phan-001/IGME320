using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastManager : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue;
    [SerializeField] private Commands commands;
    [SerializeField] private DragIn dragIn;

    public string currentAction = "";

    public Dialogue Dialogue { get { return dialogue; } }
}
