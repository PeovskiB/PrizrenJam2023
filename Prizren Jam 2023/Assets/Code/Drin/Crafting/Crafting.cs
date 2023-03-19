using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    [SerializeField]
    public List<Recipe> recipes;
    public RectTransform viewParent;
    public GameObject craftingPanel;
    public GameObject viewRecipeBase;
    void Start()
    {
        craftingPanel.gameObject.SetActive(false);
    }
    public void BuildView()
    {
        recipes.Sort(new RecipeComparer());
        foreach (Recipe r in recipes)
        {
            ViewRecipe vr = Instantiate(viewRecipeBase, viewParent).GetComponent<ViewRecipe>();
            vr.Init(r, Inventory.main_inventory);
        }
        craftingPanel.gameObject.SetActive(true);
    }
    public void ClearView()
    {
        craftingPanel.gameObject.SetActive(false);
        foreach (Transform child in viewParent)
        {
            Destroy(child.gameObject);
        }
    }
}