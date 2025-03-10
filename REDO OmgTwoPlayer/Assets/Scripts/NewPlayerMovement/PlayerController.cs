using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent (typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;

    private Vector2 movementInput = Vector2.zero;
    private bool jumped = false;

    private PlayerInput playerInput;  // Reference to PlayerInput for each player

    //Doing the stuff for handholding system:
    [SerializeField] private Transform player1;
    private GameObject player2;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();  // Get PlayerInput component

        // Check if Player 1 or Player 2
        if (playerInput.playerIndex == 0) // Player 1
        {
            // Set up Player 1 controls
            playerInput.actions["Movement"].performed += ctx => movementInput = ctx.ReadValue<Vector2>();
            playerInput.actions["Jump"].performed += ctx => jumped = true;
        }
        else if (playerInput.playerIndex == 1) // Player 2
        {
            // Set up Player 2 controls
            playerInput.actions["Movement"].performed += ctx => movementInput = ctx.ReadValue<Vector2>();
            playerInput.actions["Jump"].performed += ctx => jumped = true;
        }
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        //jumped = context.ReadValue<bool>();
        jumped = context.action.triggered;
    }

    public void OnHoldHand(InputAction.CallbackContext context)
    {
        //Check if their hands are ...holding lol:
        if (player2 != null)
        {
            player2.GetComponent<Rigidbody>().isKinematic = false; //Enabling physics??
            player2.transform.parent = null;
            isHoldingHand = false;

            //turn off UI that prompts player to hold hand
            //[insert].SetActive(false)
        }

        // Need something to check the distance between both players. Unsure if I should use raycast?
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

        // Debugging: Draw the ray in the Scene view
        Debug.DrawRay(playerCamera.position, playerCamera.forward * pickUpRange, Color.red, 2f);


        if (Physics.Raycast(ray, out hit, pickUpRange))
        {
            // Check if the hit object has the tag "Player1"
            if (hit.collider.CompareTag("Player1"))
            {
                // Hold the player
                heldObject = hit.collider.gameObject;
                heldObject.GetComponent<Rigidbody>().isKinematic = true; // Disable physics to ensure the player doesnt move

                // Attach player to left hand position. 
                heldObject.transform.position = holdPositionLeft.position;
                heldObject.transform.rotation = holdPositionLeft.rotation;
                heldObject.transform.parent = holdPositionLeft;

                //Make sure the pickuptext disappears after the object has been picked up
                //pickUpText.SetActive(false);
            }

            // Check if the hit object has the tag "Player2"
            if (hit.collider.CompareTag("Player2"))
            {
                // Hold the player
                heldObject = hit.collider.gameObject;
                heldObject.GetComponent<Rigidbody>().isKinematic = true; // Disable physics to ensure the player doesnt move

                // Attach player to left hand position. 
                heldObject.transform.position = holdPositionLeft.position;
                heldObject.transform.rotation = holdPositionLeft.rotation;
                heldObject.transform.parent = holdPositionLeft;

                //Make sure the pickuptext disappears after the object has been picked up
                //pickUpText.SetActive(false);
            }

        }
    }
  

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // Makes the player jump
        if (jumped && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
            jumped = false;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
