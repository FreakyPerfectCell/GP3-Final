using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    public void ResetScene()
    {
        Debug.Log("meow :3");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
