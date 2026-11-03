using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public InputActionAsset inputActions;
    private InputAction m_moveAction;
    private InputAction m_sprintAction;
    public CharacterController controller;

    public float walkSpeed = 12f;
    public float sprintSpeed = 20f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private bool isGrounded;
    
    private float speed = 12f;
    public float gravity = -9.81f;

    private Vector3 velocity;
    
    private Vector2 m_moveAmt;

    private void OnEnable()
    {
        inputActions.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        inputActions.FindActionMap("Player").Disable();
    }

    private void Awake()
    {
        m_moveAction = InputSystem.actions.FindAction("Move");
        m_sprintAction = InputSystem.actions.FindAction("Sprint");
        speed = walkSpeed;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        m_moveAmt = m_moveAction.ReadValue<Vector2>();
        
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        Vector3 move = transform.right * m_moveAmt.x + transform.forward * m_moveAmt.y;

        if(m_sprintAction.WasPressedThisFrame()){
            speed = sprintSpeed;
        } else if (m_sprintAction.WasReleasedThisFrame()){
            speed = walkSpeed;
        }

        controller.Move(move*speed*Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
