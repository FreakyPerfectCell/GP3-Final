using UnityEngine;

public class Player : MonoBehaviour
{

    public Rigidbody2D rb2d;
    public float moveSpeed = 2f;
    private Vector2 moveInput;
    private Vector2 mouseInput;
    public float mouseSensitivity = 1f;
    public Transform cam;

    void Start()
    {
        moveSpeed = 2f;
    }

    void Update()
    {

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 3f;
        }
        else
        {
            moveSpeed = 2f;
        }
        

        //movement crap
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector3 moveHorizontal = transform.up * -moveInput.x;
        Vector3 moveVertical = transform.right * moveInput.y;
        rb2d.linearVelocity = (moveHorizontal + moveVertical) * moveSpeed;

        //mouse crap
        mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - mouseInput.x);
        cam.localRotation = Quaternion.Euler(cam.localRotation.eulerAngles + new Vector3(0f, mouseInput.y, 0f));
    }
}
