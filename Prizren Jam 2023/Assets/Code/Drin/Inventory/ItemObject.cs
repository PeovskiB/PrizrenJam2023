using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemObject : MonoBehaviour
{
    [SerializeField]
    public Item item;
    public int minDrop, maxDrop;
    public Item drop;
    private void Start()
    {
        Init();
    }
    private void Init()
    {
        transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = item.data.icon;
        string txt = "";
        if (item.quantity > 1)
            txt = item.quantity.ToString();
        GetComponentInChildren<TextMeshProUGUI>().text = txt;
        item.OnDurabilityChange += CheckDurability;
    }
    private void CheckDurability(int newD)
    {
        if (newD == 0) Break();
    }
    public void Break()
    {
        if (item.data.type == ItemType.Breakable)
        {
            int d = Random.Range(minDrop, maxDrop);
            ItemObject new_item_object = Instantiate(Inventory.main_inventory.data.item_object, transform.position, transform.rotation).GetComponent<ItemObject>();
            new_item_object.Drop(drop, d);
        }
        Destroy(gameObject);
        // Play break sound
    }
    public void Drop(Item new_item, int qty = -1)
    {
        if (new_item == null)
        {
            item = null;
            return;
        }
        item = new Item(new_item);
        if (qty != -1)
            item.quantity = qty;
        Init();
    }
    public void Equip()
    {
        Destroy(gameObject);
    }
}