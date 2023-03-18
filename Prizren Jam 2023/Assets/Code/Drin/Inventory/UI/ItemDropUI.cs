using UnityEngine;
using UnityEngine.EventSystems;
public class ItemDropUI : MonoBehaviour, IDropHandler
{
    public InventoryUI inventory_ui;
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null){
            ItemUI new_item = eventData.pointerDrag.GetComponent<ItemUI>();
            if (!new_item) return;

            if (new_item.slot) inventory_ui.RemoveItem(new_item); 
        }
    }
}