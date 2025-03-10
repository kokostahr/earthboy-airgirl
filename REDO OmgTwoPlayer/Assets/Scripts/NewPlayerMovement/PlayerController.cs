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
