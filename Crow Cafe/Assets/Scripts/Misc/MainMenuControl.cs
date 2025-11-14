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
        PlayerPrefs.DeleteKey("Tutorial_Cafe_Shown");
        PlayerPrefs.DeleteKey("Tutorial_Dorm_Shown");

        PlayerPrefs.DeleteKey("DormDecorations");

        PlayerPrefs.Save();

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
