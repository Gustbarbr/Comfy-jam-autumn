using UnityEngine;
using UnityEngine.SceneManagement;

public class SleepButtonControl : MonoBehaviour
{
    public void ExitDorm()
    {
        DormSaveManager.Instance.SaveDecorations();
        SceneManager.LoadScene("Cafe");
    }
}
