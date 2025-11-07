using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ClockControl : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI clockText;
    [SerializeField] float remainingTime;

    private void Update()
    {
        if(remainingTime > 0)
            remainingTime -= Time.deltaTime;

        if(remainingTime <= 0)
        {
            remainingTime = 0;
            SceneManager.LoadScene("Dorm");
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        clockText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
