using UnityEngine;
public class Hands : MonoBehaviour
{
    public Item left, right;
    public ItemSlotUI slot_left, slot_right;
    public Item start_left, start_right;
    private void Start(){
        slot_left.OnItemChange += SetLeft;
        slot_right.OnItemChange += SetRight;
        SetLeft(start_left);
        SetRight(start_right);
    }
    private void OnDisable(){
        slot_left.OnItemChange -= SetLeft;
        slot_right.OnItemChange -= SetRight;
    }
    private void SetLeft(Item new_item, ItemType type = ItemType.Hand){
        left = new_item;
    }
    private void SetRight(Item new_item, ItemType type = ItemType.Hand){
        right = new_item;
    }
}
