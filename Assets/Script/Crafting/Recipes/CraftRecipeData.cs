using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CraftRecipeData", menuName = "Crafting/Craft Recipe Data")]
public class CraftRecipeData : ScriptableObject
{
    public ItemData resultItem;
    public int resultAmount = 1;

    [System.Serializable]
    public struct Ingredient
    {
        public ItemData item;
        public int amount;
    }

    public List<Ingredient> ingredients;
}
