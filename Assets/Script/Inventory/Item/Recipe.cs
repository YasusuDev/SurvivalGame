
using UnityEngine;

[CreateAssetMenu(fileName = "NewRecipe", menuName = "Scriptable Objects/Recipe")]
public class Recipe : ScriptableObject
{
    public Item result; // Item que será criado (ex: Axe)
    public Item[] requiredItems; // Itens necessários
    public int[] requiredAmounts; // Quantidade necessária de cada item
}


