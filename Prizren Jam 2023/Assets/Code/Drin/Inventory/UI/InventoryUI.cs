using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public struct InventoryUIData
{
    public bool should_build;
    public int max_slots;
    public Transform[] build_from;
    public Transform ui_parent;
    public Transform top_ui_parent;
    public GameObject item_ui;
    public GameObject slot;
    public GameObject drop;
};
public class InventoryUI : MonoBehaviour
{
    public static InventoryUI instance;
    public Inventory inventory;
    [SerializeField]
    public InventoryUIData data;
    public List<ItemSlotUI> slots;
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    private void Start()
    {
        inventory.OnAdd += AddItem;
        slots = new List<ItemSlotUI>(data.max_slots);

        foreach (Transform t in data.build_from)
        {
            ItemDropUI drop = t.GetComponentInChildren<ItemDropUI>();
            if (drop != null)
                drop.inventory_ui = this;

            foreach (ItemSlotUI sl in t.GetComponentsInChildren<ItemSlotUI>()) slots.Add(sl);
        }

        if (data.should_build)
        {
            for (int i = 0; i < data.max_slots - slots.Count; i++) slots.Add(Instantiate(data.slot, data.ui_parent).GetComponent<ItemSlotUI>());
            Instantiate(data.drop, data.ui_parent).GetComponent<ItemDropUI>().inventory_ui = this;
        }
    }
    private void OnDisable()
    {
        if (inventory != null)
            inventory.OnAdd -= AddItem;
    }
    public void AddItem(Item new_item)
    {
        foreach (ItemSlotUI slot in slots)
        {
            if (!slot.CanTake(new_item)) continue;
            ItemUI i = Instantiate(data.item_ui).GetComponent<ItemUI>();
            i.Setup(new_item, data.top_ui_parent);
            slot.SetItem(i);
            return;
        }
    }
    public void RemoveItem(ItemUI item)
    {
        foreach (ItemSlotUI slot in slots)
            if (slot.HasItem(item))
            {
                slot.SetItem(null);
                inventory.RemoveItem(item.data);
                Destroy(item.gameObject);
            }
    }
}