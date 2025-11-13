using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControl : MonoBehaviour
{
    public GameObject credits;

    private void Start()
    {
        credits.SetActive(false);
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Cafe");
    }

    public void Credits()
    {
        credits.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
