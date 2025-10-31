using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClientControl : MonoBehaviour
{
    [Header("Spawn)")]
    public List<Transform> spawn;
    private bool alreadySpawned = false;
    public float speed;
    private Transform chosenSpawn;

    [Header("Request")]
    public List<Transform> food;
    private bool alreadyRequested = false;
    public GameObject chosenFood;

    void Start()
    {
        if(alreadySpawned == false)
        {
            List<Transform> availableSpawns = new List<Transform>();

            foreach (Transform s in spawn) {
                SpawnPoint spawnPoint = s.GetComponent<SpawnPoint>();
                if (spawnPoint != null && spawnPoint.occupied == false)
                    availableSpawns.Add(s);
            }

            if(availableSpawns.Count > 0)
            {
                int choosePoint = Random.Range(0, availableSpawns.Count);
                chosenSpawn = availableSpawns[choosePoint];
                chosenSpawn.GetComponent<SpawnPoint>().occupied = true;
                transform.position = chosenSpawn.position;
                alreadySpawned = true;
            }
        }

        if(alreadyRequested == false)
        {
            int chooseFood = Random.Range(0, food.Count);
            chosenFood = food[chooseFood].gameObject;
            chosenFood.SetActive(true);
            Vector2 foodRequestPosition = new Vector2(transform.position.x, transform.position.y + 2f);
            chosenFood.transform.position = foodRequestPosition;
            alreadyRequested = true;
        }
    }
}
