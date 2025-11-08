using UnityEngine;

public class DecorationManager : MonoBehaviour
{
    public static DecorationManager Instance;

    [Header("References")]
    public PlayerControl player;
    public PlayerDecorInventory inventory;

    [Header("Decoration Mode")]
    public bool isDecorating = false;
    public DecorationItem currentItem;
    private GameObject ghost;
    public LayerMask obstacleMask;

    [Header("Materials")]
    public Material validMat;
    public Material invalidMat;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        if (player == null)
            player = FindObjectOfType<PlayerControl>();

        if (inventory == null)
            inventory = FindObjectOfType<PlayerDecorInventory>();
    }

    void Update()
    {
        if (!isDecorating) return;
        HandlePlacement();
    }
    public void EnterDecorationMode(DecorationItem item)
    {
        if (inventory == null || player == null)
        {
            return;
        }

        if (inventory.CanPlace(item))
        {
            currentItem = item;
            isDecorating = true;
            player.canMove = false;

            ghost = Instantiate(item.prefab);
            SetGhostMaterial(validMat);
        }
    }

    public void ExitDecorationMode()
    {
        if (ghost != null)
            Destroy(ghost);

        isDecorating = false;
        currentItem = null;

        if (player != null)
            player.canMove = true;
    }
    private void HandlePlacement()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        ghost.transform.position = mousePos;

        bool canPlace = CanPlaceHere();
        SetGhostMaterial(canPlace ? validMat : invalidMat);

        if (Input.GetKeyDown(KeyCode.R))
            ghost.transform.Rotate(0, 0, 90f);

        if (Input.GetMouseButtonDown(0) && canPlace)
            PlaceDecoration();

        if (Input.GetKeyDown(KeyCode.Escape))
            ExitDecorationMode();
    }
    private bool CanPlaceHere()
    {
        Collider2D col = ghost.GetComponent<Collider2D>();
        if (col == null) return true;

        Collider2D[] hits = Physics2D.OverlapBoxAll(
            col.bounds.center,
            col.bounds.size * 0.9f,
            0,
            obstacleMask
        );

        return hits.Length == 0;
    }
    private void PlaceDecoration()
    {
        GameObject placed = Instantiate(currentItem.prefab, ghost.transform.position, ghost.transform.rotation);
        placed.tag = "Decoration";
        placed.AddComponent<DecorationObject>().item = currentItem;

        inventory.MarkPlaced(currentItem);
        ExitDecorationMode();
    }

    private void SetGhostMaterial(Material mat)
    {
        SpriteRenderer sr = ghost.GetComponent<SpriteRenderer>();
        if (sr != null)
            sr.material = mat;
    }
}
