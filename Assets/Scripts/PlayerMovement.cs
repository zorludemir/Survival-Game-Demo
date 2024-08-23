using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float gravity = -9.81f;

    [SerializeField]bool isSprint = false;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private Player player;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        player = GetComponent<Player>();
    }

    void Update()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * moveSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        
        if (Input.GetKey(KeyCode.LeftShift)  && !isSprint) 
        {
            moveSpeed = 10f;
            player.staminaRegen = -20;
            if(player.currentStamina <= 0)
            {
                isSprint = true;
            }
        }
        else
        {
            moveSpeed = 5f;
            player.staminaRegen = 10;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift) && isSprint)
        {
            isSprint = false;
        }
    }
}
