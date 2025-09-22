using UnityEngine;
using UnityEditor;

[System.Serializable]
public class Ingredient
{
    public ItemData item;
    public int amount;
}

[CreateAssetMenu(fileName = "CraftRecipeData", menuName = "Crafting/Recipe")]
public class CraftRecipeData : ScriptableObject
{
    [Header("Craft Ingredients")]
    public Ingredient[] ingredients;

    [Header("Crafting Result")] 
    public ItemData result;
    public int resultAmount = 1;
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(Ingredient))]
public class IngredientDrawer : PropertyDrawer
{
    private const float Padding = 4f;
    private const float LabelWidth = 80f; // largura fixa para o nome do campo

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        SerializedProperty itemProp = property.FindPropertyRelative("item");
        SerializedProperty amountProp = property.FindPropertyRelative("amount");

        float totalWidth = position.width - Padding * 2;
        float amountWidth = 40f; // largura fixa para quantidade
        float itemFieldWidth = totalWidth - LabelWidth - amountWidth - Padding * 2;

        var labelRect = new Rect(position.x + Padding, position.y, LabelWidth, EditorGUIUtility.singleLineHeight);
        var itemRect = new Rect(labelRect.x + LabelWidth, position.y, itemFieldWidth, EditorGUIUtility.singleLineHeight);
        var amountRect = new Rect(itemRect.x + itemFieldWidth + Padding, position.y, amountWidth, EditorGUIUtility.singleLineHeight);

        // Rótulo do ingrediente
        string ingredientName = itemProp.objectReferenceValue != null
            ? ((ItemData)itemProp.objectReferenceValue).itemName
            : "Item";
        EditorGUI.LabelField(labelRect, ingredientName);

        // Campo ObjectField do ItemData
        EditorGUI.ObjectField(itemRect, itemProp, typeof(ItemData), GUIContent.none);

        // Quantidade
        EditorGUI.PropertyField(amountRect, amountProp, GUIContent.none);

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight + Padding;
    }
}
#endif