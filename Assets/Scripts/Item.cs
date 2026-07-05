using UnityEngine;
[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]
public class Item : ScriptableObject
{
    public int ID;
    public string itemName;
    public float Value;
    public Sprite icon;

    public ItemType itemType;

    public enum ItemType
    {
        Coin,
        Heart

    };

}

