using UnityEngine;
using System;
[Serializable]
public class Item
{
    public string name;
    public int quantity = 1;
    public int durability;
    public ItemData data;
    public delegate void IntAction(int new_quantity);
    public event IntAction OnQuantityChange;
    public event IntAction OnDurabilityChange;
    public delegate void UseAction(Item i);
    public event UseAction OnUse;
    public Item(string n, int qty, int dur, ItemData id)
    {
        name = n;
        quantity = qty;
        durability = dur;
        data = id;
    }
    public Item(Item other)
    {
        name = other.name;
        quantity = other.quantity;
        durability = other.durability;
        data = other.data;
    }
    public int QuantityAdd(int amount)
    {
        int new_qty = quantity + amount;
        quantity = Mathf.Clamp(new_qty, 0, data.max_quantity);
        if (OnQuantityChange != null)
            OnQuantityChange(quantity);
        return new_qty > data.max_quantity ? new_qty - data.max_quantity : new_qty < 0 ? -new_qty : 0;
    }
    public int DurabilityTake(int amount)
    {
        durability = Mathf.Clamp(durability - amount, 0, durability);
        if (OnDurabilityChange != null)
            OnDurabilityChange(durability);
        return durability;
    }
    public void Use()
    {
        if (OnUse != null)
            OnUse(this);
    }
}