using UnityEngine;
using UnityEngine.SceneManagement;

public class StartApp_Logo : MonoBehaviour
{
    private void Start()
    {
        Invoke("LoadNextScene", 2f);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(1);
    }
}
