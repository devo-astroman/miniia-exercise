using UnityEngine;

public class FreeFlyCamera : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 10f;
    public float fastMultiplier = 3f;

    [Header("Mouse Look")]
    public float lookSensitivity = 2f;

    private float yaw;
    private float pitch;

    public bool lockOnClick = true;

    void Start()
    {
        // Initialize rotation from current transform
        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;

    }

    void Update()
    {
        HandleMouseLook();
        HandleMovement();

        if (lockOnClick && Cursor.lockState != CursorLockMode.Locked)
        {
            // Any real user gesture works:
            if (Input.GetMouseButtonDown(0) || Input.anyKeyDown)
            {
                LockCursor();
            }
        }

        // Optional: press Esc to unlock
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnlockCursor();
        }
    }

    void HandleMouseLook()
    {
        // Only rotate while right mouse is pressed (optional)
        if (!Input.GetMouseButton(1)) return;

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        yaw += mouseX * lookSensitivity;
        pitch -= mouseY * lookSensitivity;
        pitch = Mathf.Clamp(pitch, -89f, 89f);

        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }

    void HandleMovement()
    {
        float speed = moveSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
            speed *= fastMultiplier;

        Vector3 move = Vector3.zero;
        move += transform.forward * Input.GetAxis("Vertical");   // W/S
        move += transform.right   * Input.GetAxis("Horizontal"); // A/D

        if (Input.GetKey(KeyCode.E)) move += Vector3.up;
        if (Input.GetKey(KeyCode.Q)) move += Vector3.down;

        transform.position += move * speed * Time.deltaTime;
    }

    public void LookAt(Vector3 targetPosition)
    {
        //can you complete this function to make the camera look at a target position?
        Vector3 direction = targetPosition - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = targetRotation;
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
