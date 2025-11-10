using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseScreenButton : MonoBehaviour
{
    PlayerControl player;
    public GameObject screenToClose;

    private void Start()
    {
        player = FindObjectOfType<PlayerControl>();
    }

    public void CloseScreen()
    {
        player.canMove = true;
        screenToClose.SetActive(false);
    }
}
