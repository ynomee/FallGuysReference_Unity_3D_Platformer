using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class ApplicationInteractions : MonoBehaviour
{
    protected virtual void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    protected virtual void ExitGame()
    {
        Application.Quit();
    }
}
