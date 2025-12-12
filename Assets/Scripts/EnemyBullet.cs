using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    public int damageAmount;
    public float bulletSpeed = 3f;
    public Rigidbody2D rb2d;
    private Vector3 direction;


    void Start()
    {
        direction = Player.instance.transform.position - transform.position;
        direction.Normalize();
        direction = direction * bulletSpeed;
    }

    void Update()
    {
        rb2d.linearVelocity = direction * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player.instance.TakeDamage(damageAmount);
            Destroy(gameObject);
        }
    }
}
