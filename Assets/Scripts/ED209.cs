using UnityEngine;
using UnityEngine.SceneManagement;

public class ED209 : MonoBehaviour
{

    public int health = 10000;

    public float shootRange = 10f;
    public float playerRange = 15f;
    public Rigidbody2D rb;
    public float moveSpeed;

    public bool shouldShoot;
    public float fireRate = .7f;
    private float shotCounter;
    public GameObject bullet;
    public Transform firePoint;

    void Update()
    {
        if(Vector3.Distance(transform.position, Player.instance.transform.position) < playerRange)
        {
            Vector3 playerDirection = Player.instance.transform.position - transform.position;
            rb.linearVelocity = playerDirection.normalized * moveSpeed;
            if(shouldShoot)
            {
                shotCounter -= Time.deltaTime;
                if(shotCounter <= 0)
                {
                    Instantiate(bullet, firePoint.position, firePoint.rotation);
                    shotCounter = fireRate;
                }
            }
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    public void TakeDamage()
    {
        AudioManager.instance.PlayEnemyShot();
        health--;
        if(health <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene("Ending");
            //other functions i want to happen
        }
    }

    public void TakeSniperDamage()
    {
        AudioManager.instance.PlayEnemyExplode();
        health -= 50;
        if(health <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene("Ending");
            //other functions i want to happen
        }
    }
}
