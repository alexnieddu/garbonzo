using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    
    
    private CharacterController controller;
    private Vector3 playerVelocity;
   
    
    public bool groundedPlayer;
    
    private Vector2 movementInput = Vector2.zero;

    private static int numPlayers = 0;
    private  int playerNum;
    
    public bool jumped = false;
    

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        numPlayers++;
        playerNum = numPlayers;
        Debug.Log("Added new player!");
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        jumped = context.action.triggered;
    }
    void Update()
    {
        if (playerNum == 1)
        {
            Debug.Log("Yeha, I am Player 1");
            gravityValue = 0;
        }

        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);
        controller.Move(move * Time.deltaTime * playerSpeed);
        
        
        // Changes the height position of the player..
        if (jumped && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}