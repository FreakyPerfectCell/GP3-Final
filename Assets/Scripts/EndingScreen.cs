using UnityEngine;

public class EndingScreen : MonoBehaviour
{

    public GameObject endScreen;
    public float timer = 0f;

    void Awake()
    {
        endScreen.SetActive(false);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 5.5f)
        {
            endScreen.SetActive(true);
        }
    }
}
