using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DecorationInventory
{
    public DecorationItem item;
    public int owned;
    public int placed;
}

public class PlayerDecorInventory : MonoBehaviour
{
    public List<DecorationInventory> inventory = new List<DecorationInventory>();

    public void AddItem(DecorationItem item)
    {
        var found = inventory.Find(i => i.item == item);
        if (found != null)
            found.owned++;
        else
            inventory.Add(new DecorationInventory { item = item, owned = 1, placed = 0 });
    }

    public bool CanPlace(DecorationItem item)
    {
        var found = inventory.Find(i => i.item == item);
        return found != null && found.placed < found.owned;
    }

    public void MarkPlaced(DecorationItem item)
    {
        var found = inventory.Find(i => i.item == item);
        if (found != null) found.placed++;
    }

    public void MarkRemoved(DecorationItem item)
    {
        var found = inventory.Find(i => i.item == item);
        if (found != null && found.placed > 0) found.placed--;
    }

}
