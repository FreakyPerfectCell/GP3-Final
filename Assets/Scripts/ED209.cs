using UnityEngine;

public class ED209 : MonoBehaviour
{

    public int health = 1000;

    public float playerRange = 10f;
    public Rigidbody2D rb;
    public float moveSpeed;

    public bool shouldShoot;
    public float fireRate = .5f;
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
        health--;
        if(health <= 0)
        {
            Destroy(gameObject);
            //other functions i want to happen
        }
    }

    public void TakeSniperDamage()
    {
        health -= 50;
        if(health <= 0)
        {
            Destroy(gameObject);
            //other functions i want to happen
        }
    }
}
