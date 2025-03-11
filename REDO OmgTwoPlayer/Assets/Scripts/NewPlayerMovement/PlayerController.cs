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
    [SerializeField] 
    private Transform player1;
    [SerializeField] 
    private Transform player2;
    [SerializeField]
    private float holdHandDistance = 3f;
    public bool isHoldingHand = false;
    public float followSpeed = 5f;
    [SerializeField]
    GameObject handHoldPrompt;


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
        if (context.performed && Vector3.Distance(player1.position, player2.position) <= holdHandDistance)
        {
            //Turn on and off the hand holding
            isHoldingHand = !isHoldingHand;

            if (isHoldingHand)
            {
                //Disable player 2's movement
                player2.GetComponent<PlayerController>().enabled = false;
                //Attach player 2 to player 1, even if it looks crecrrakerouse
                //idk what i was gonna write, took a brek
                player2.parent = player1; //Attaching player wan to player 2.
                //might make an empty object for this a bit later, so that it looks smoother.
                //why is player 2 being dragged LOL. Kitten helped with this part:
                //player2.GetComponent<CharacterController>().Move(Vector3.zero);
                //when their hands are holding, turn off movement completely^
                //player2.GetComponent<CharacterController>().detectCollisions = false;
                //to apparently 'avoid weird physics'
            }
            else //when they let go
            {
                //Enable player 2's movement again. 
                player2.GetComponent<PlayerController>().enabled = true;
                //Remove player 2 from player 1
                player2.parent = null;
                //when the hands let go, disable that effect on player 2's ridigbody
                //player2.GetComponent<CharacterController>().detectCollisions = true;
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

        //more fixes by kitten, cuz why is it dragging?
        float gravityMultiplier = isHoldingHand ? 1.5f : 1f; //increases the gravity while holding hands
        float speedMultiplier = isHoldingHand ? 1.3f : 1f; //INcrease speed when hands held

        playerVelocity.y += (gravityValue * gravityMultiplier) * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        //something for player 2 to follow player as if being dragged or pulled. 
        if (isHoldingHand)
        {
            transform.position = Vector3.Lerp(transform.position, player1.position - player1.forward * 1f, Time.deltaTime * followSpeed);
        }

        //Want the UI TO SHOW UP WHENEVER THE PLAYERS ARE IN CLOSE PROXIMITY and aren't holding hands
        if (Vector3.Distance(player1.position, player2.position) <= holdHandDistance && !isHoldingHand)
        {
            handHoldPrompt.SetActive(true);
        }
        else
        {
            handHoldPrompt.SetActive(false);
        }

    }
}
