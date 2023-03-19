using System.Collections;
using UnityEngine;
public class Hands : MonoBehaviour
{
    public static Hands instance;
    public InventoryUI inventoryUI;
    public Item main;
    public ItemSlotUI mainSlot;
    public Item startMain;
    private ItemObject mainObject;
    public GameObject baseObject;
    public Transform drop;
    public float useRadius;
    public LayerMask itemsLayer;
    public CircleCollider2D useCheckCollider;
    private void OnDrawGizmos()
    {
        if (drop == null) return;
        Gizmos.DrawWireSphere(drop.position, useRadius);
    }
    private void Start()
    {
        mainSlot.OnItemChange += SetMain;
        AddToUI(startMain);
        StartCoroutine(UpdateRoutine());
    }
    private void OnDisable()
    {
        mainSlot.OnItemChange -= SetMain;
        main.OnUse -= OnUse;
    }
    private void SetMain(Item new_item, ItemType type = ItemType.Usable)
    {
        if (new_item == null)
        {
            main = null;
            if (mainObject)
                Destroy(mainObject.gameObject);
            return;
        }
        else
            main = new Item(new_item);
        if (!mainObject)
        {
            mainObject = Instantiate(baseObject, drop.position, drop.rotation, drop).GetComponent<ItemObject>();
        }
        mainObject.Drop(new_item);
        main.OnUse += OnUse;
    }

    private void AddToUI(Item item)
    {
        if (!mainSlot.CanTake(item)) return;
        ItemUI i = Instantiate(inventoryUI.data.item_ui).GetComponent<ItemUI>();
        i.Setup(item, inventoryUI.data.top_ui_parent);
        mainSlot.SetItem(i);
        return;
    }
    private void UnSet()
    {
        if (mainSlot) inventoryUI.RemoveItem(mainSlot.item);
        main.OnUse -= OnUse;
        main = null;
    }
    private void OnUse(Item i)
    {
        if (i.DurabilityTake(1) == 0) UnSet();
    }
    /*
    any for usable tools:

    0: break_per / attack_per
    1: break_dur / attack_dmg

    */
    private IEnumerator UpdateRoutine()
    {
        float c = 0;
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Collider2D[] hitCols = Physics2D.OverlapCircleAll(drop.position, useRadius, itemsLayer);
                foreach (Collider2D col in hitCols)
                {
                    ItemObject io = col.GetComponent<ItemObject>();
                    if (io == null || mainObject == io) continue;
                    Inventory.main_inventory.AddItem(io);
                    // io.Equip((int)main.data.any_properties[1]);
                }

            }
            if (main != null && main.data != null)
            {
                if (MouseCheckRadius.IsDown())
                {
                    c += Time.deltaTime;
                    print("Clickin");
                }
                else
                {
                    c = 0;
                }
                if (c >= main.data.any_properties[0])
                {
                    if (main == null || main.data == null) continue;
                    c = 0;
                    main.Use();
                    Collider2D[] hitCols = Physics2D.OverlapCircleAll(drop.position, useRadius, main.data.use_layer);
                    foreach (Collider2D col in hitCols)
                    {
                        ItemObject io = col.GetComponent<ItemObject>();
                        if (io == null) continue;
                        io.item.DurabilityTake((int)main.data.any_properties[1]);
                    }
                }
            }
            else
            {
                c = 0;
            }

            yield return null;
        }
    }
}
