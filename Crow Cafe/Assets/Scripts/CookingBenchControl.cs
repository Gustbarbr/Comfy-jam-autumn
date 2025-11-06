using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingBenchControl : MonoBehaviour
{
    PlayerControl player;
    public GameObject cookSystem;
    public CookPotControl cookPotControl;

    private void Start()
    {
        player = FindObjectOfType<PlayerControl>();
    }

    private void Update()
    {
        if (Vector2.Distance(player.transform.position, transform.position) <= 0.4f)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                cookSystem.SetActive(true);

                if (cookPotControl == null)
                    cookPotControl = cookSystem.GetComponentInChildren<CookPotControl>(true);

                cookPotControl.OpenCookPot();
            }
        }
    }
}
