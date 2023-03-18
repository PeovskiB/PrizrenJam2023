using UnityEngine;
using System.Collections.Generic;
public class Gear : MonoBehaviour
{
    public Dictionary<ItemType, Item> gear_items;
    public ItemSlotUI[] gear_slots;
    [SerializeField]
    public Item[] starting_gear;
    private void Start(){
        gear_items = new Dictionary<ItemType, Item>(3);
        foreach (ItemSlotUI slot in gear_slots) slot.OnItemChange += AddItem;
        foreach (Item i in starting_gear) AddItem(i, i.data.type);
    }
    private void OnDisable(){
        foreach (ItemSlotUI slot in gear_slots) slot.OnItemChange -= AddItem;
    }
    public Item GetHead(){
        return gear_items[ItemType.Head];
    }
    public Item GetBody(){
        return gear_items[ItemType.Body];
    }
    public Item GetLegs(){
        return gear_items[ItemType.Legs];
    }
    private void AddItem(Item new_item, ItemType type){
        gear_items[type] = new_item;
    }
}