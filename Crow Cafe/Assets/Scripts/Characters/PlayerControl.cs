using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    private static PlayerControl instance;

    public float speed;
    private Rigidbody2D rb;
    public List<Transform> items;
    public GameObject cookMenu;
    public bool canMove = true;
    public Transform dormSpawnPoint;

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

    [Header("UI Texts")]
    public TMP_Text txtCoins;

    public TMP_Text txtCoffeeBeans;
    public TMP_Text txtBread;
    public TMP_Text txtMilk;
    public TMP_Text txtSugar;
    public TMP_Text txtFlour;
    public TMP_Text txtEgg;

    public TMP_Text txtCake;
    public TMP_Text txtCoffeeWithMilk;
    public TMP_Text txtBreadWithEgg;
    public TMP_Text txtGlassOfMilk;
    public TMP_Text txtGlassOfCoffee;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Dorm" && dormSpawnPoint != null)
            transform.position = dormSpawnPoint.position;
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
        UpdateInventoryUI();

        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Cafe")
        {
            if (Input.GetKeyDown(KeyCode.Tab))
                cookMenu.SetActive(true);
            else if (Input.GetKeyUp(KeyCode.Tab))
                cookMenu.SetActive(false);
        }
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
                if(Vector2.Distance(transform.position, i.position) <= 1.5f)
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


    public void UpdateInventoryUI()
    {
        if (txtCoins != null) txtCoins.text = "x" + coins.ToString();

        if (txtCoffeeBeans != null) txtCoffeeBeans.text = "x" + coffeeBeans.ToString();
        if (txtBread != null) txtBread.text = "x" + bread.ToString();
        if (txtMilk != null) txtMilk.text = "x" + milk.ToString();
        if (txtSugar != null) txtSugar.text = "x" + sugar.ToString();
        if (txtFlour != null) txtFlour.text = "x" + flour.ToString();
        if (txtEgg != null) txtEgg.text = "x" + egg.ToString();

        if (txtCake != null) txtCake.text = "x" + cake.ToString();
        if (txtCoffeeWithMilk != null) txtCoffeeWithMilk.text = "x" + coffeeAndMilk.ToString();
        if (txtBreadWithEgg != null) txtBreadWithEgg.text = "x" + breadWithEgg.ToString();
        if (txtGlassOfMilk != null) txtGlassOfMilk.text = "x" + glassWithMilk.ToString();
        if (txtGlassOfCoffee != null) txtGlassOfCoffee.text = "x" + glassWithCoffee.ToString();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(RebuildSceneReferences());
    }

    private IEnumerator RebuildSceneReferences()
    {
        yield return null;

        cookMenu = FindInactiveObjectByName("Cook Menu");

        if (SceneManager.GetActiveScene().name == "Dorm")
        {
            GameObject spawnObj = GameObject.Find("DormSpawnPoint");
            if (spawnObj != null)
                dormSpawnPoint = spawnObj.transform;

            transform.position = dormSpawnPoint.position;
        }

        items = new List<Transform>();

        GameObject[] itensCena = Resources.FindObjectsOfTypeAll<GameObject>();

        foreach (GameObject obj in itensCena)
        {
            if (obj.CompareTag("Ingredient"))
                items.Add(obj.transform);
        }


        txtCoins = FindComponentInScene<TMP_Text>("Coin Quantity Text");
        txtCoffeeBeans = FindComponentInScene<TMP_Text>("Coffee Txt");
        txtBread = FindComponentInScene<TMP_Text>("Bread Txt");
        txtMilk = FindComponentInScene<TMP_Text>("Milk txt");
        txtSugar = FindComponentInScene<TMP_Text>("Sugar Txt");
        txtFlour = FindComponentInScene<TMP_Text>("Flour Txt");
        txtEgg = FindComponentInScene<TMP_Text>("Egg Txt");

        txtCake = FindComponentInScene<TMP_Text>("Cake Txt");
        txtCoffeeWithMilk = FindComponentInScene<TMP_Text>("Milk and coffee Txt");
        txtBreadWithEgg = FindComponentInScene<TMP_Text>("Bread with egg Txt");
        txtGlassOfMilk = FindComponentInScene<TMP_Text>("Glass with milk Txt");
        txtGlassOfCoffee = FindComponentInScene<TMP_Text>("Glass with coffee Txt");

    }

    private GameObject FindInactiveObjectByName(string name)
    {
        var allObjects = Resources.FindObjectsOfTypeAll<GameObject>();

        foreach (var obj in allObjects)
        {
            if (obj.name == name && obj.scene.IsValid())
                return obj;
        }

        return null;
    }

    private T FindComponentInScene<T>(string objectName) where T : Component
    {
        T[] allComponents = Resources.FindObjectsOfTypeAll<T>();

        foreach (var comp in allComponents)
        {
            if (comp.name == objectName && comp.gameObject.scene.IsValid())
                return comp;
        }

        return null;
    }


}
