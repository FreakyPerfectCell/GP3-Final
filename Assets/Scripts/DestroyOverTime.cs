using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{

    public float lifetime;

    void Update()
    {
        Destroy(gameObject, lifetime);
    }
}
