using UnityEngine;

public class DecorationObject : MonoBehaviour
{
    public DecorationItem item;

    private void OnMouseDown()
    {
        if (!DecorationManager.Instance.isDecorating)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                DecorationManager.Instance.inventory.MarkRemoved(item);
                Destroy(gameObject);
            }
        }
    }
}
