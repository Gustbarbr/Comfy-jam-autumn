using UnityEngine;

public class DecorationShop : MonoBehaviour
{
    private PlayerControl player;
    private PlayerDecorInventory inventory;
    private DecorationManager decoManager;
    public GameObject decoStore;

    private void Start()
    {
        player = FindObjectOfType<PlayerControl>();
        inventory = FindObjectOfType<PlayerDecorInventory>();
        decoManager = FindObjectOfType<DecorationManager>();
    }

    public void BuyDecoration(DecorationItem item)
    {
        if (player == null || inventory == null || decoManager == null)
            return;

        if (player.coins >= item.price)
        {
            player.coins -= item.price;
            inventory.AddItem(item);
            player.UpdateInventoryUI();

            decoStore.SetActive(false);
            player.canMove = true;
            decoManager.EnterDecorationMode(item);
        }
    }
}
