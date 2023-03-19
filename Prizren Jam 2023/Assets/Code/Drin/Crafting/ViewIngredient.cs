using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Ingredient
{
    public ItemData i;
    public int qty;
    public bool Has(Inventory iv)
    {
        return iv.HasItemQty(i, qty);
    }
}


public class ViewIngredient : MonoBehaviour
{
    public Image img;
    public TextMeshProUGUI qty;
    public Ingredient ingredient;
    public GameObject disabler;
    public void Init(Ingredient newIng, bool enabled = true)
    {
        ingredient = newIng;
        img.sprite = ingredient.i.icon;
        qty.text = ingredient.qty.ToString();
        disabler.SetActive(!enabled);
    }
}
