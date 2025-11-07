using UnityEngine;

public class Billboard : MonoBehaviour
{

    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.flipX = true;
    }

    void Update()
    {
        transform.LookAt(Player.instance.transform.position, -Vector3.forward);
    }
}
