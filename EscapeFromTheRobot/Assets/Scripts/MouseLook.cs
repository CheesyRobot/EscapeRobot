using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class MouseLook : MonoBehaviour
{
    public InputActionAsset inputActions;

    private InputAction m_lookAction;
    private Vector2 m_lookAmt;

    public float mouseSensitivity = 100f;
    public Transform playerBody;

    float xRotation = 0f;

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
        m_lookAction = InputSystem.actions.FindAction("Look");
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        m_lookAmt = m_lookAction.ReadValue<Vector2>() * mouseSensitivity * Time.deltaTime;

        xRotation -= m_lookAmt.y ;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * m_lookAmt.x);
    }
}
