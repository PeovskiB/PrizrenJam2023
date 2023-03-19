using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Recipe
{
    public List<Ingredient> ingredients;
    public ItemData result;
    public bool CanCraft(Inventory i)
    {
        foreach (Ingredient ig in ingredients)
            if (!ig.Has(i)) return false;
        return true;
    }
}


public class ViewRecipe : MonoBehaviour
{
    public Recipe recipe;
    public RectTransform recipeView;
    public GameObject ingredientBase;
    public GameObject btn;
    public void Craft()
    {
        if (!recipe.CanCraft(Inventory.main_inventory)) return;

        foreach (Ingredient i in recipe.ingredients)
            Inventory.main_inventory.RemoveItemQty(i.i, i.qty);

        Inventory.main_inventory.AddItem(recipe.result);
    }
    public void Init(Recipe newRecipe, Inventory iv)
    {
        recipe = newRecipe;
        foreach (Ingredient i in recipe.ingredients)
        {
            ViewIngredient vi = Instantiate(ingredientBase, recipeView).GetComponent<ViewIngredient>();
            vi.Init(i, i.Has(iv));
        }
        Button but = Instantiate(btn, recipeView).GetComponent<Button>();
        but.onClick.AddListener(Craft);
        but.interactable = recipe.CanCraft(Inventory.main_inventory);
    }
}
