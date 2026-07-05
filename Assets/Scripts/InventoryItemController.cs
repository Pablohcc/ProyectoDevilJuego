using UnityEngine;

public class InventoryItemController : MonoBehaviour
{

    Item item;

    public void AddItem(Item newItem)

    {
        item = newItem;
    }

    public void UseItem()
    {
        if (item == null) return;

        Item.ItemType tipo = item.itemType;
        float valor = item.Value;
        

        RemoveItem();
        switch (tipo)
        {
            case Item.ItemType.Heart:
                VidaJugador.Instance.VidaUp(valor);
                break;
            
        }
    }

    public void RemoveItem()
    {
        InventoryManager.Instance.Remove(item);
        Destroy(gameObject);
    }
}


