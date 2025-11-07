using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientControl : MonoBehaviour
{
    PlayerControl player;

    [Header("Spawn")]
    public List<Transform> spawn;
    private Transform chosenSpawn;

    [Header("Request")]
    public List<Transform> food;
    public GameObject chosenFood;

    private bool orderDelivered = false;

    void Awake()
    {
        player = FindObjectOfType<PlayerControl>();
    }

    void OnEnable()
    {
        orderDelivered = false;
        chosenFood = null;

        foreach (Transform f in food)
            if (f != null) f.gameObject.SetActive(false);

        List<Transform> availableSpawns = new List<Transform>();
        foreach (Transform s in spawn)
        {
            SpawnPoint spawnPoint = s.GetComponent<SpawnPoint>();
            if (spawnPoint != null && !spawnPoint.occupied)
                availableSpawns.Add(s);
        }

        if (availableSpawns.Count > 0)
        {
            int choosePoint = Random.Range(0, availableSpawns.Count);
            chosenSpawn = availableSpawns[choosePoint];
            chosenSpawn.GetComponent<SpawnPoint>().occupied = true;
            transform.position = chosenSpawn.position;
        }

        int chooseFood = Random.Range(0, food.Count);
        chosenFood = food[chooseFood].gameObject;
        chosenFood.SetActive(true);
        Vector2 foodRequestPosition = new Vector2(transform.position.x, transform.position.y + 2f);
        chosenFood.transform.position = foodRequestPosition;
    }

    private void Update()
    {
        if (player == null) return;

        if (player.transform.position.x > transform.position.x)
            transform.localScale = new Vector2(1, 1);
        else if (player.transform.position.x < transform.position.x)
            transform.localScale = new Vector2(-1, 1);

        if (!orderDelivered && Vector2.Distance(player.transform.position, transform.position) < 2f)
        {
            if (Input.GetKeyDown(KeyCode.E))
                TryDeliverOrder();
        }
    }

    private void TryDeliverOrder()
    {
        if (chosenFood == null) return;

        string requestedItem = chosenFood.name.Replace("(Clone)", "").Trim();

        int goldReward = 0;
        bool delivered = false;

        switch (requestedItem)
        {
            case "Bread with egg":
                if (player.breadWithEgg > 0)
                {
                    player.breadWithEgg--;
                    goldReward = 2;
                    delivered = true;
                }
                break;

            case "Cake":
                if (player.cake > 0)
                {
                    player.cake--;
                    goldReward = 5;
                    delivered = true;
                }
                break;

            case "Coffee and Milk":
                if (player.coffeeAndMilk > 0)
                {
                    player.coffeeAndMilk--;
                    goldReward = 2;
                    delivered = true;
                }
                break;

            case "Glass with milk":
                if (player.glassWithMilk > 0)
                {
                    player.glassWithMilk--;
                    goldReward = 1;
                    delivered = true;
                }
                break;

            case "Glass with coffee":
                if (player.glassWithCoffee > 0)
                {
                    player.glassWithCoffee--;
                    goldReward = 1;
                    delivered = true;
                }
                break;
        }

        if (delivered)
        {
            player.coins = Mathf.Min(player.coins + goldReward, 99);
            orderDelivered = true;
            chosenFood.SetActive(false);

            StartCoroutine(LeaveAfterDelay());
        }
    }

    private IEnumerator LeaveAfterDelay()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
    }
}
