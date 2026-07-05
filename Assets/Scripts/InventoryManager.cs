// InventoryManager.cs
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public InventoryItemController[] InventoryItems;
    public Transform ItemContent;
    public GameObject InventoryItem;
    public List<Item> Items = new List<Item>();
    public static InventoryManager Instance;

    void Awake()
    {
        Instance = this;
    }

    public void ListItems()
    {
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);

            // Búsqueda segura con null check
            Transform nameTransform = obj.transform.Find("ItemName");
            Transform iconTransform = obj.transform.Find("ItemIcon");

            if (nameTransform == null || iconTransform == null)
            {
                Debug.LogError("El prefab InventoryItem no tiene hijos 'ItemName' o 'ItemIcon'. Revisa los nombres exactos.");
                continue;
            }

            var itemName = nameTransform.GetComponent<Text>();
            var itemIcon = iconTransform.GetComponent<Image>();

            if (itemName == null || itemIcon == null)
            {
                Debug.LogError("Falta componente Text o Image en el prefab InventoryItem.");
                continue;
            }

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;
            obj.GetComponent<InventoryItemController>().AddItem(item);
        }
    }

    public void Add(Item item)
    {
        Items.Add(item);
    }

    public void Remove(Item item)
    {
        Items.Remove(item);
    }
}