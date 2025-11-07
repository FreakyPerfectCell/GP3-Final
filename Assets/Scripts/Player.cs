using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    public static Player instance;
    public Rigidbody2D rb2d;
    public float moveSpeed = 2f;
    private Vector2 moveInput;
    private Vector2 mouseInput;
    public float mouseSensitivity = 1f;
    public Camera cam;
    public GameObject bulletImpact;
    public int currentAmmo;

    public Animator roboAnim;

    private void Awake ()
    {
        instance = this;
    }


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

        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < 200)
        {
            StartCoroutine(ReloadSequence());
        }
        

        //movement crap
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector3 moveHorizontal = transform.up * -moveInput.x;
        Vector3 moveVertical = transform.right * moveInput.y;
        rb2d.linearVelocity = (moveHorizontal + moveVertical) * moveSpeed;

        //mouse crap
        mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - mouseInput.x);
        cam.transform.localRotation = Quaternion.Euler(cam.transform.localRotation.eulerAngles + new Vector3(0f, mouseInput.y, 0f));

        //shooting crap
        if (Input.GetMouseButton(0) && currentAmmo > 0)
        {
            roboAnim.SetBool("isShooting", true);
            Ray ray = cam.ViewportPointToRay(new Vector3(.5f, .5f, 0f));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Instantiate(bulletImpact, hit.point, transform.rotation);
                //Debug.Log("Im looking at " + hit.transform.name);
            }
            else
            {
                Debug.Log("Im looking at nothing");
            }
            currentAmmo--;
        }
        else
        {
            roboAnim.SetBool("isShooting", false);
        }

        IEnumerator ReloadSequence()
        {
            roboAnim.SetTrigger("isReloading");
            yield return new WaitForSeconds(1.1f);
            Reload();
        }

        void Reload()
        {
            currentAmmo = 200;
        }
    }
}
