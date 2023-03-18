using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestControls : MonoBehaviour
{
    public LayerMask lm;
    public void Shot()
    {
        Ray pos = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D h = Physics2D.Raycast(pos.origin, pos.direction, 10000, lm);
        if (!h) return;
        ItemObject new_pickup = h.transform.GetComponent<ItemObject>();
        if (!new_pickup) return;
        Inventory.main_inventory.AddItem(new_pickup);
    }
}
