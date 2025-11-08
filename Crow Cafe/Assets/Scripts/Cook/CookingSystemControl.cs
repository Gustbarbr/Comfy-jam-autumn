using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingSystemControl : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 startPos;
    private CookPotControl currentPot;
    private PlayerControl player;

    private void Start()
    {
        startPos = transform.position;
        player = FindObjectOfType<PlayerControl>();
    }

    private void OnMouseDown()
    {
        string ingredient = gameObject.name;
        if (!PlayerHasIngredient(ingredient))
            return;

        isDragging = true;
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Mathf.Abs(Camera.main.transform.position.z);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

            transform.position = new Vector3(worldPos.x, worldPos.y, transform.position.z);
        }
    }

    private void OnMouseUp()
    {
        if (isDragging && currentPot != null)
            currentPot.AddIngredient(gameObject.name);

        isDragging = false;
        transform.position = startPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CookPot"))
            currentPot = collision.GetComponent<CookPotControl>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("CookPot"))
            currentPot = null;
    }

    private bool PlayerHasIngredient(string name)
    {
        switch (name)
        {
            case "Coffee Beans": return player.coffeeBeans > 0;
            case "Bread": return player.bread > 0;
            case "Milk": return player.milk > 0;
            case "Sugar": return player.sugar > 0;
            case "Flour": return player.flour > 0;
            case "Egg": return player.egg > 0;
            default: return false;
        }
    }
}
