using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CookPotControl : MonoBehaviour
{
    [Header("Cooking System UI")]
    public GameObject cookPotUI;

    private PlayerControl playerControl;
    private bool isOpen = false;

    public List<string> currentIngredients = new List<string>();

    private Dictionary<HashSet<string>, string> recipes = new Dictionary<HashSet<string>, string>(HashSetComparer<string>.Instance)
    {
        { new HashSet<string> { "Bread", "Egg" }, "Bread with egg" },
        { new HashSet<string> { "Milk", "Sugar", "Flour", "Egg" }, "Cake" },
        { new HashSet<string> { "Coffee Beans", "Milk" }, "Coffee and Milk" },
        { new HashSet<string> { "Milk" }, "Glass with milk" },
        { new HashSet<string> { "Coffee Beans" }, "Glass with coffee" },
    };

    private void Start()
    {
        playerControl = FindObjectOfType<PlayerControl>();
        cookPotUI.SetActive(false);
    }

    private void Update()
    {
        if (isOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseCookPot();
        }
    }

    public void OpenCookPot()
    {
        cookPotUI.SetActive(true);
        isOpen = true;
        playerControl.canMove = false;
    }

    public void CloseCookPot()
    {
        cookPotUI.SetActive(false);
        isOpen = false;
        playerControl.canMove = true;
    }


    public void OnCloseButtonPressed()
    {
        CloseCookPot();
    }

    public void AddIngredient(string ingredient)
    {
        if (!RemoveFromInventory(ingredient))
            return;

        currentIngredients.Add(ingredient);

        bool hasPossibleRecipe = recipes.Keys.Any(r => currentIngredients.All(i => r.Contains(i)));

        if (!hasPossibleRecipe)
            currentIngredients.Clear();
    }

    public void ConfirmMix()
    {
        if (currentIngredients.Count == 0)
            return;

        foreach (var recipe in recipes)
        {
            if (recipe.Key.SetEquals(currentIngredients))
            {
                string result = recipe.Value;
                GiveToPlayer(result);
                currentIngredients.Clear();
                return;
            }
        }
        ReturnIngredientsToInventory();
        currentIngredients.Clear();
    }

    private void ReturnIngredientsToInventory()
    {
        foreach (string ingredient in currentIngredients)
        {
            switch (ingredient)
            {
                case "Coffee Beans": playerControl.coffeeBeans++; break;
                case "Bread": playerControl.bread++; break;
                case "Milk": playerControl.milk++; break;
                case "Sugar": playerControl.sugar++; break;
                case "Flour": playerControl.flour++; break;
                case "Egg": playerControl.egg++; break;
            }
        }
    }

    private bool RemoveFromInventory(string ingredient)
    {
        switch (ingredient)
        {
            case "Coffee Beans": if (playerControl.coffeeBeans > 0) { playerControl.coffeeBeans--; return true; } break;
            case "Bread": if (playerControl.bread > 0) { playerControl.bread--; return true; } break;
            case "Milk": if (playerControl.milk > 0) { playerControl.milk--; return true; } break;
            case "Sugar": if (playerControl.sugar > 0) { playerControl.sugar--; return true; } break;
            case "Flour": if (playerControl.flour > 0) { playerControl.flour--; return true; } break;
            case "Egg": if (playerControl.egg > 0) { playerControl.egg--; return true; } break;
        }
        return false;
    }

    private void GiveToPlayer(string result)
    {
        switch (result)
        {
            case "Bread with egg": playerControl.breadWithEgg++; break;
            case "Cake": playerControl.cake++; break;
            case "Coffee and Milk": playerControl.coffeeAndMilk++; break;
            case "Glass with milk": playerControl.glassWithMilk++; break;
            case "Glass with coffee": playerControl.glassWithCoffee++; break;
        }
    }
}

public class HashSetComparer<T> : IEqualityComparer<HashSet<T>>
{
    public static readonly HashSetComparer<T> Instance = new HashSetComparer<T>();

    public bool Equals(HashSet<T> x, HashSet<T> y) => x.SetEquals(y);

    public int GetHashCode(HashSet<T> obj)
    {
        int hash = 0;
        foreach (T t in obj)
            hash ^= t.GetHashCode();
        return hash;
    }
}
