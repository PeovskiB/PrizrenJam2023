using UnityEngine;
using System;
[Serializable]
public class Item
{
    public string name;
    public int quantity = 1;
    public int durability;
    public ItemData data;
    public delegate void QuantityAction(int new_quantity);
    public event QuantityAction OnQuantityChange;

    public Item(Item other){
        name = other.name;
        quantity = other.quantity;
        durability = other.durability;
        data = other.data;
    }

    public int QuantityAdd(int amount){
        int new_qty = quantity + amount;
        quantity = Mathf.Clamp(new_qty, 0, data.max_quantity);
        if (OnQuantityChange != null)
            OnQuantityChange(quantity);
        return new_qty > data.max_quantity ? new_qty - data.max_quantity : new_qty < 0 ? -new_qty : 0;
    }
}