using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public DialogueReader objDialogue;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    [Header("Player Attributes")]
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private float interactRange = 10f;
    [SerializeField] RaycastManager raycastManager;

    private InputManager inputManager;
    private InteractionBrain interactionBrain;
    private Transform cameraTransform;
    private Dialogue dialogue;
    private Commands commands;

    private bool uiCreated = false;

    private void Start()
    {
        inputManager = InputManager.Instance;
        controller = GetComponent<CharacterController>();


        try {
            interactionBrain = InteractionBrain.Instance;
        }
        catch
        {
            Debug.Log("No UI setup");
        }
        
        cameraTransform = Camera.main.transform;
        if(raycastManager != null)
        {
            dialogue = raycastManager.Dialogue;
            commands = raycastManager.Commands;
            uiCreated = true;
        }
        

    }

    void Update()
    {

        if (!dialogue.InDialogue) //disable movement when in dialogue
        {
            // ---------------------- Movement logic --------------------------
            groundedPlayer = controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }
            Vector2 movement = inputManager.GetPlayerMovement();
            Vector3 move = new Vector3(movement.x, 0f, movement.y);
            move.y = 0;
            move = cameraTransform.forward * move.z + cameraTransform.right * move.x;

            controller.Move(move * Time.deltaTime * playerSpeed);

            // Changes the height position of the player..
            //if (inputManager.PlayerJumped() && groundedPlayer)
            //{
            //    playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            //}
            // -----------------------------------------------------------------
        }

        //When player presses interact, what we do with respect to what object we interact with
        if (inputManager.Interact())
        {
            RaycastHit hit;
            if (dialogue.InDialogue)
            {
                if (!dialogue.Writing)
                {
                    dialogue.PrintSentence();
                    commands.NextCommand();
                }
                else
                {
                    dialogue.ChangePrintSpeed(0f);
                }
            
            }
            if (!dialogue.Writing && !dialogue.InDialogue)
            {
                if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, interactRange) && hit.transform != this.transform)
                {
                    try
                    {
                        string curr = null;
                        DialogueReader objDialogue = hit.transform.GetComponent<DialogueReader>();
                        if (objDialogue.Responses.ContainsKey(raycastManager.currentAction)) //if the current action is an action available for our obj
                        {
                            //this assigns the dialogue system to use this new series of text
                            curr = raycastManager.currentAction;
                        }
                        // if not, check for other cases
                        // introduction
                        else if (raycastManager.currentAction == "" && objDialogue.Responses.ContainsKey("introduction") && !objDialogue.hasIntroduced)
                        {
                            curr = "introduction";
                            objDialogue.hasIntroduced = true;
                        }
                        // default filler afterwards
                        else if (raycastManager.currentAction == "" && objDialogue.Responses.ContainsKey("filler") && objDialogue.hasIntroduced)
                        {
                            //this assigns the dialogue system to use this new series of text
                            curr = "filler";
                        }
                        // bad evidence
                        else if (objDialogue.Responses.ContainsKey("badEvidence"))
                        {
                            curr = "badEvidence";
                        }

                        // will catch if theres actually nothing to say.
                        dialogue.AssignNewResponse(objDialogue.Responses[curr]);
                        commands.AssignCommands(objDialogue.Commands[curr]);
                        //we do print sentence right away just for testing
                        dialogue.PrintSentence();
                        commands.NextCommand();
                    }
                    catch
                    {
                        Debug.Log($"Generic: {hit.transform.name}");
                    }
                }
            }
            
        }
        if (inputManager.InteractHold()) Debug.Log("Holding main");
        //When player opens the inventory
        if (inputManager.Inventory())
        {
            //Debug.Log("Inventory clicked");
            if(!dialogue.Writing) interactionBrain.SwitchBrainState();

        }
        //*
        //if (inputManager.Next())
        //{
        //    if(uiCreated)
        //    {
        //        if (!dialogue.Writing)
        //        {
        //            dialogue.PrintSentence();
        //            commands.NextCommand();
        //        }
        //    }
        //}

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}