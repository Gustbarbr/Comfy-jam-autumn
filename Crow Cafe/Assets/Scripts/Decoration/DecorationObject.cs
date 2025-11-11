using UnityEngine;

public class DecorationObject : MonoBehaviour
{
    public DecorationItem item;
    PlayerControl player;

    private void Start()
    {
        player = FindObjectOfType<PlayerControl>();
    }

    private void OnMouseDown()
    {
        if (!DecorationManager.Instance.isDecorating)
        {
            if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftShift))
            {
                DecorationManager.Instance.inventory.MarkRemoved(item);
                player.coins += item.price;
                Destroy(gameObject);
            }
        }
    }
}
