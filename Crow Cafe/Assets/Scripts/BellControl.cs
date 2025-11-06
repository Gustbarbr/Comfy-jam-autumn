using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BellControl : MonoBehaviour
{
    public List<Transform> spawnableIngredients;
    public List<Transform> actualIngredients;
    public List<Transform> spawnpoints;
    PlayerControl player;

    private void Start()
    {
        player = FindObjectOfType<PlayerControl>();
    }

    private void Update()
    {
        if(Vector2.Distance(player.transform.position, transform.position) <= 2f)
            if(Input.GetKeyDown(KeyCode.E))
                if(actualIngredients.Count > 0)
                {
                    for(int i = 0; i <  actualIngredients.Count; i++)
                    {
                        actualIngredients[i].gameObject.SetActive(false);
                    }
                    actualIngredients.Clear();
                }

        if (actualIngredients.Count <= 0)
        {
            List<int> usedIngredients = new List<int>();
            for (int j = 0; j < 3; j++)
            {
                int randomIngredient;
                do
                {
                    randomIngredient = Random.Range(0, spawnableIngredients.Count);
                }
                while (usedIngredients.Contains(randomIngredient));

                usedIngredients.Add(randomIngredient);
                actualIngredients.Add(spawnableIngredients[randomIngredient]);
                Transform ingredient = actualIngredients[j];

                ingredient.position = spawnpoints[j].position;
                actualIngredients[j].gameObject.SetActive(true);
            }
        }
    }
}
