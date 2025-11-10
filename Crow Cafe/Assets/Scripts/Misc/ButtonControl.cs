using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControl : MonoBehaviour
{
    PlayerControl player;
    public GameObject screenToOpen;

    private void Start()
    {
        player = FindObjectOfType<PlayerControl>();
        screenToOpen.SetActive(false);
    }

    public void OpenScreen()
    {
        screenToOpen.SetActive(true);
        player.canMove = false;
    }
}
