using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookMenuControl : MonoBehaviour
{
    PlayerControl player;

    private void Start()
    {
        player = FindObjectOfType<PlayerControl>();
    }

    private void Update()
    {
        transform.position = new Vector2(player.transform.position.x, player.transform.position.y);
    }
}
