using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public struct InventoryData {
    public int max_items;
    public GameObject item_object;
    public Transform drop_transform;
}
public class Inventory : MonoBehaviour
{
    public static Inventory main_inventory;
    [SerializeField]
    public InventoryData data;
    private List<Item> items;
    public delegate void ItemAddAction(Item new_item);
    public event ItemAddAction OnAdd;
    public delegate void ItemRemoveAction(Item item);
    public event ItemRemoveAction OnRemove;
    private void Awake(){
        if (main_inventory == null)
            main_inventory = this;
    }
    private void Start(){
        items = new List<Item>();
    }
    public void AddItem(ItemObject new_item_object){
        if (new_item_object == null) return;

        Item new_item = new_item_object.item;
        Item item = items.Find(x => x.name == new_item.name && x.quantity < x.data.max_quantity);

        if (item != null)
            new_item.quantity = item.QuantityAdd(item.QuantityAdd(new_item.quantity));

        if (new_item.quantity > 0 && items.Count < data.max_items){
            Item added_item = new Item(new_item);
            items.Add(added_item);
            new_item_object.Equip();

            if (OnAdd != null)
                OnAdd(added_item);
        }
        else if (new_item.quantity == 0)
            new_item_object.Equip();
    }
    public void RemoveItem(Item item){

        if (items.Remove(item))
        {
            Vector2 new_pos = data.drop_transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
            ItemObject new_item_object = Instantiate(data.item_object, new_pos, data.drop_transform.rotation).GetComponent<ItemObject>();
            new_item_object.Drop(item);
            if (OnRemove != null)
                OnRemove(item);
        }
    }
}