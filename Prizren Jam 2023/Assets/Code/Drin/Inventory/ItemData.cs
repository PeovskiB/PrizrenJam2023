using UnityEngine;
using System;

[Serializable]
public enum ItemType {
    General,
    Head,
    Body,
    Legs,
    Hand,
    Resource
}

[CreateAssetMenu(fileName = "new item", menuName = "items/new item", order = 1)]
public class ItemData : ScriptableObject {
    public ItemType type;
    public int max_quantity;
    public int max_durability;
    public Sprite icon;
}