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

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        inputManager = InputManager.Instance;
        interactionBrain = InteractionBrain.Instance;
        cameraTransform = Camera.main.transform;
        dialogue = raycastManager.Dialogue;

    }

    void Update()
    {
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

        if (inputManager.Interact())
        {
            RaycastHit hit;
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, interactRange) && hit.transform != this.transform)
            {
                try
                {
                    DialogueReader objDialogue = hit.transform.GetComponent<DialogueReader>();
                    if (objDialogue.Responses.ContainsKey(raycastManager.currentAction)) //if the current action is an action available for our obj
                    {
                        //this assigns the dialogue system to use this new series of text
                        dialogue.AssignNewResponse(objDialogue.Responses[raycastManager.currentAction]);

                        //we do print sentence right away just for testing
                        dialogue.PrintSentence();
                    }
                }
                catch
                {
                    Debug.Log($"Generic: {hit.transform.name}");
                }
            }
        }

        if (inputManager.Inventory())
        {
            //Debug.Log("Inventory clicked");
            interactionBrain.SwitchBrainState();
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}