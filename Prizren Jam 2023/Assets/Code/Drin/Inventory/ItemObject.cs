using TMPro;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField]
    public Item item;
    private void Start()
    {
        Init();
    }
    private void Init()
    {
        GetComponent<SpriteRenderer>().sprite = item.data.icon;
        GetComponentInChildren<TextMeshProUGUI>().text = item.quantity.ToString();
    }
    public void Drop(Item new_item)
    {
        item = new_item;
        Init();
    }
    public void Equip()
    {
        Destroy(gameObject);
    }

    public void OnMouseDown()
    {
        Inventory.main_inventory.AddItem(this);
    }
}