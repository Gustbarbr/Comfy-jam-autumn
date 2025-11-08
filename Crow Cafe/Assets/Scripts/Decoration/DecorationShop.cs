using UnityEngine;

public class DecorationShop : MonoBehaviour
{
    private PlayerControl player;
    private PlayerDecorInventory inventory;

    private void Start()
    {
        player = FindObjectOfType<PlayerControl>();
        inventory = FindObjectOfType<PlayerDecorInventory>();
    }

    public void BuyDecoration(DecorationItem item)
    {
        if (player == null || inventory == null)
        {
            return;
        }

        if (player.coins >= item.price)
        {
            player.coins -= item.price;
            inventory.AddItem(item);
            player.UpdateInventoryUI();
        }
    }
}
