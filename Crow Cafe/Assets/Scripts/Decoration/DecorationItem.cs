using UnityEngine;

[CreateAssetMenu(fileName = "NewDecoration", menuName = "Decorations/Decoration Item")]
public class DecorationItem : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public GameObject prefab;
    public int price;
}
