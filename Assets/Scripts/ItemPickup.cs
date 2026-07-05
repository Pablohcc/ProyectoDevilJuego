// ItemPickup.cs
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item Item;

    public void Pickup()
    {
        if (InventoryManager.Instance == null)
        {
            Debug.LogError("InventoryManager no encontrado en la escena.");
            return;
        }

        InventoryManager.Instance.Add(Item);
        InventoryManager.Instance.ListItems();
        Destroy(gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Pickup(); 
        }
    }
}