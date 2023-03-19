using System.Collections.Generic;

public class RecipeComparer : IComparer<Recipe>
{
    public int Compare(Recipe x, Recipe y)
    {
        int byWhole = y.CanCraft(Inventory.main_inventory).CompareTo(x.CanCraft(Inventory.main_inventory));
        if (byWhole != 0)
            return byWhole;
        // Calculate the number of ingredients each recipe has
        int xCount = CountIngredients(x);
        int yCount = CountIngredients(y);

        // If both recipes have the same number of ingredients, compare their total quantity
        if (xCount == yCount)
        {
            int xQty = TotalQuantity(x);
            int yQty = TotalQuantity(y);
            return xQty.CompareTo(yQty);
        }

        // Otherwise, sort in descending order based on the number of ingredients
        return xCount.CompareTo(yCount);
    }

    private int CountIngredients(Recipe recipe)
    {
        int count = 0;
        foreach (var ingredient in recipe.ingredients)
        {
            if (ingredient.i != null) // Make sure the ingredient is not null
                count++;
        }
        return count;
    }

    private int TotalQuantity(Recipe recipe)
    {
        int total = 0;
        foreach (var ingredient in recipe.ingredients)
        {
            if (ingredient.i != null) // Make sure the ingredient is not null
                total += ingredient.qty;
        }
        return total;
    }
}