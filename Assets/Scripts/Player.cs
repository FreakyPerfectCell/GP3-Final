using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    public static Player instance;
    private int gunChoice;
    public GameObject[] roboHands;

    [Header("Misc Crap")]
    public bool pistolUp = true; 
    public int currentHealth;
    public int maxHealth = 100;
    public GameObject deadScreen;
    private bool hasDied;
    
    [Header("Player Crap")]
    public Rigidbody2D rb2d;
    public float moveSpeed = 2f;
    private Vector2 moveInput;
    private Vector2 mouseInput;
    public float mouseSensitivity = 1f;
    public Camera cam;

    [Header("Pistol Crap")]
    public GameObject bulletImpact;
    public int currentAmmo;
    public Animator roboAnim;

    [Header("Sniper Crap")]
    public GameObject sniperImpact;
    public float cooldown = 30f;
    public float timer = 0f;
    public Animator roboAnim2;


    private void Awake ()
    {
        instance = this;
        roboHands[0].SetActive(true);
        roboHands[1].SetActive(false);
        pistolUp = true;
    }

    void Start()
    {
        currentHealth = maxHealth;
        moveSpeed = 2f;
        roboAnim.SetTrigger("start");
        timer = cooldown;
    }

    void Update()
    {
        if(!hasDied)
        { 
            timer += Time.deltaTime;
            //moveSpeed crap
            if (Input.GetKey(KeyCode.LeftShift))
            {
                moveSpeed = 3f;
            }
            else
            {
                moveSpeed = 2f;
            }

            //reload order
            if (Input.GetKeyDown(KeyCode.R) && (currentAmmo < 200) && (pistolUp == true))
            {
                StartCoroutine(ReloadSequence());
            }

            //weapon swap crap
            if (Input.GetKey(KeyCode.Alpha1) && (pistolUp == false))
            {
                roboAnim.SetTrigger("sniperPistol");
                StartCoroutine(swapCooldown());
                roboHands[0].SetActive(true);
                roboHands[1].SetActive(false);
                pistolUp = true;

            }
            if (Input.GetKey(KeyCode.Alpha2) && (pistolUp == true))
            {
                roboAnim.SetTrigger("pistolSniper");
                StartCoroutine(swapCooldown());
                roboHands[0].SetActive(false);
                roboHands[1].SetActive(true);
                pistolUp = false;
                roboAnim.SetTrigger("sniperWake");
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
            //pistol
            if (pistolUp == true)
            {
                if (Input.GetMouseButton(0) && currentAmmo > 0)
                {
                    //pistol sfx
                    roboAnim.SetBool("isShooting", true);
                    Ray ray = cam.ViewportPointToRay(new Vector3(.5f, .5f, 0f));
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        Instantiate(bulletImpact, hit.point, transform.rotation);
                        //Debug.Log("Im looking at " + hit.transform.name);

                        if(hit.transform.tag == "Enemy")
                        {
                            hit.transform.parent.GetComponent<ED209>().TakeDamage();
                        }
                    }
                    else
                    {
                        //Debug.Log("Im looking at nothing");
                    }
                    currentAmmo--;
                }
                else
                {
                    roboAnim.SetBool("isShooting", false);
                }
            }

            //lets the reload anim play before reloading
            IEnumerator ReloadSequence()
            {
                roboAnim.SetTrigger("isReloading");
                yield return new WaitForSeconds(1.1f);
                Reload();
            }

            //reloads
            void Reload()
            {
                currentAmmo = 200;
            }

            IEnumerator swapCooldown()
            {
                yield return new WaitForSeconds(1.1f);
            }

            //sniper
            if (pistolUp == false)
            {
                if (Input.GetMouseButtonDown(0) && timer >= cooldown)
                {
                    //sniper sfx
                    roboAnim.SetTrigger("sniperShot");
                    Ray ray = cam.ViewportPointToRay(new Vector3(.5f, .5f, 0f));
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        Instantiate(sniperImpact, hit.point, transform.rotation);
                        //Debug.Log("Im looking at " + hit.transform.name);

                        if(hit.transform.tag == "Enemy")
                        {
                            hit.transform.parent.GetComponent<ED209>().TakeSniperDamage();
                        }

                        if (timer >= cooldown)
                        {
                            timer = 0f;
                        }
                    }
                    StartCoroutine(swapCooldown());
                    roboAnim.SetTrigger("sniperPistol");
                    pistolUp = true;
                    roboHands[0].SetActive(true);
                    roboHands[1].SetActive(false);
                    StartCoroutine(swapCooldown());
                }
            }

            // moving animation stuff
            if (moveInput != Vector2.zero)
            {
                roboAnim.SetBool("isMoving", true);
            }
            else
            {
                roboAnim.SetBool("isMoving", false);
            }
        }
    }
    
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        //richochet sfx
        if(currentHealth <= 0)
        {
            deadScreen.SetActive(true);
            hasDied = true;
        }
    }

    public void AddHealth(int healAmount)
    {
        currentHealth += healAmount;
        //heal sfx
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
