using UnityEngine;

public class CraftingSystem : MonoBehaviour, IInteractable
{
    public CraftRecipeData craftRecipe;
    private Inventory _inventory;

    private bool _isActive = true;
    
    public bool Interact(GameObject interactor)
    {
        _inventory = interactor.GetComponent<Inventory>();
        if (_inventory is null)
        {
            Debug.LogWarning("Interactor has no Inventory!");
            return false;
        }

        return Craft(craftRecipe);
    }
    
    public bool IsActive()
    {
        return _isActive;
    }
    
    public bool Craft(CraftRecipeData recipe)
    {
        foreach (var ingredient in recipe.ingredients)
        {
            if (!_inventory.HasEnough(ingredient.item, ingredient.amount))
            {
                Debug.Log("Missing items to craft " + recipe.resultItem.itemName);
                return false;
            }
        }

        foreach (var ingredient in recipe.ingredients)
        {
            _inventory.RemoveItems(ingredient.item, ingredient.amount);
        }
        
        _inventory.AddItem(recipe.resultItem, recipe.resultAmount);

        Debug.Log("Successfully crafted: " + recipe.resultItem.itemName);
        return true;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        _isActive = other.GetComponentInParent<CharacterController>() != null;
    }
}