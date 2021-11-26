using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
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
        controller = GetComponent<CharacterController>();


        try {
            inputManager = InputManager.Instance;
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

        //When player presses interact, what we do with respect to what object we interact with
        if (inputManager.Interact())
        {
            RaycastHit hit;
            if (!dialogue.Writing)
            {
                if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, interactRange) && hit.transform != this.transform)
                {
                    try
                    {
                        DialogueReader objDialogue = hit.transform.GetComponent<DialogueReader>();
                        if (objDialogue.Responses.ContainsKey(raycastManager.currentAction)) //if the current action is an action available for our obj
                        {
                            //this assigns the dialogue system to use this new series of text
                            string curr = raycastManager.currentAction;
                            dialogue.AssignNewResponse(objDialogue.Responses[curr]);
                            commands.AssignCommands(objDialogue.Commands[curr]);
                            //we do print sentence right away just for testing
                            dialogue.PrintSentence();
                            commands.NextCommand();
                        }
                    }
                    catch
                    {
                        Debug.Log($"Generic: {hit.transform.name}");
                    }
                }
            }
            
        }

        //When player opens the inventory
        if (inputManager.Inventory())
        {
            //Debug.Log("Inventory clicked");
            if(!dialogue.Writing) interactionBrain.SwitchBrainState();

        }

        if (inputManager.Next())
        {
            if(uiCreated)
            {
                if (!dialogue.Writing)
                {
                    dialogue.PrintSentence();
                    commands.NextCommand();
                }
            }
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}