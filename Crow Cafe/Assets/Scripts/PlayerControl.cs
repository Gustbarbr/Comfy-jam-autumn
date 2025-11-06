using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    public List<Transform> items;
    public GameObject cookMenu;
    public bool canMove = true;

    [Header("Items")]
    public int coins = 0;
    public int coffeeBeans = 0;
    public int bread = 0;
    public int milk = 0;
    public int sugar = 0;
    public int flour = 0;
    public int egg = 0;

    public int breadWithEgg = 0;
    public int cake = 0;
    public int coffeeAndMilk = 0;
    public int glassWithMilk = 0;
    public int glassWithCoffee = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!canMove)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        PlayerMovement();
        PickUpItems();

        if (Input.GetKeyDown(KeyCode.Tab))
            cookMenu.SetActive(true);
        else if (Input.GetKeyUp(KeyCode.Tab))
            cookMenu.SetActive(false);
    }


    public void PlayerMovement()
    {
        if (!canMove)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");

        if (horizontalMovement > 0)
            transform.localScale = new Vector2(1, 1);
        if (horizontalMovement < 0)
            transform.localScale = new Vector2(-1, 1);

        rb.velocity = new Vector2(horizontalMovement * speed, verticalMovement * speed);
    }


    public void PickUpItems()
    {
        foreach (Transform i in items)
        {
            if(i != null)
                if(Vector2.Distance(transform.position, i.position) < 0.5f)
                    if (Input.GetKeyDown(KeyCode.E) && i.gameObject.activeSelf == true)
                    {
                        if(i.name == "Coffee Beans")
                            coffeeBeans++;
                        if (i.name == "Bread")
                            bread++;
                        if (i.name == "Milk")
                            milk++;
                        if (i.name == "Sugar")
                            sugar++;
                        if (i.name == "Flour")
                            flour++;
                        if (i.name == "Egg")
                            egg++;
                        i.gameObject.SetActive(false);
                    }
        }
    }
}
