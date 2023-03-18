using UnityEngine;
using UnityEngine.EventSystems;
public class ItemSlotUI : MonoBehaviour, IDropHandler
{
    public delegate void ItemSlotAction(Item new_item, ItemType new_type);
    public event ItemSlotAction OnItemChange;
    [SerializeField]
    public ItemType accept_type = ItemType.General;
    public RectTransform rect_trans;
    public ItemUI item;
    private void Start()
    {
        rect_trans = GetComponent<RectTransform>();
    }
    public void PivotItem(ItemUI item)
    {
        item.rect_trans.transform.SetParent(rect_trans);
        item.rect_trans.anchorMin = Vector2.one / 2;
        item.rect_trans.anchorMax = Vector2.one / 2;
        item.rect_trans.pivot = Vector2.one / 2;
        item.rect_trans.anchoredPosition = new Vector2(0, 0);
    }
    public bool HasItem(ItemUI check_item = null)
    {
        return (check_item == null ? item != null : item == check_item);
    }
    public bool CanTake(Item item)
    {
        return !HasItem() && (accept_type == ItemType.General || accept_type == item.data.type);
    }
    public void SetItem(ItemUI new_item)
    {
        if (OnItemChange != null)
            OnItemChange(new_item != null ? new_item.data : null, item != null ? item.data.data.type : new_item.data.data.type);

        if (new_item == null)
        {
            item = null;
            return;
        }
        PivotItem(new_item);
        new_item.slot = this;
        item = new_item;
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            ItemUI new_item = eventData.pointerDrag.GetComponent<ItemUI>();
            if (!new_item) return;

            if (new_item.slot)
            {
                if ((accept_type != ItemType.General && new_item.data.data.type != accept_type) || (new_item.slot.accept_type != ItemType.General && item != null && item.data.data.type != new_item.slot.accept_type))
                {
                    new_item.ResetPosition();
                    if (item) item.ResetPosition();
                    return;
                }
                new_item.slot.SetItem(item);
            }
            SetItem(new_item);
        }
    }
}