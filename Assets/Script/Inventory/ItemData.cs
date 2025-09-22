using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Inventory/ItemData")]
public class ItemData : ScriptableObject
{
    public Sprite itemSprite;
    public string itemName;
    [TextArea] public string itemDescription;
    public ItemCategory itemCategory;
    public bool isStackable;
    public int maxStackSize = 1;
}

public enum ItemCategory
{
    Resource,
    Tool,
    Consumable,
    Weapon
}

#if UNITY_EDITOR
[CustomEditor(typeof(ItemData))]
public class ItemDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        var itemSpriteProp = serializedObject.FindProperty("itemSprite");
        var itemNameProp = serializedObject.FindProperty("itemName");
        var itemCategoryProp = serializedObject.FindProperty("itemCategory");
        var itemDescriptionProp = serializedObject.FindProperty("itemDescription");
        var isStackableProp = serializedObject.FindProperty("isStackable");
        var maxStackSizeProp = serializedObject.FindProperty("maxStackSize");

        EditorGUILayout.PropertyField(itemSpriteProp);
        EditorGUILayout.PropertyField(itemNameProp);
        EditorGUILayout.PropertyField(itemCategoryProp);
        EditorGUILayout.PropertyField(itemDescriptionProp);
        EditorGUILayout.PropertyField(isStackableProp);

        // Ajusta maxStackSize dependendo de isStackable
        if (isStackableProp.boolValue)
        {
            EditorGUILayout.PropertyField(maxStackSizeProp);
            if (maxStackSizeProp.intValue < 1)
                maxStackSizeProp.intValue = 1; // garante valor mínimo de 1
        }
        else
        {
            maxStackSizeProp.intValue = 1;
            EditorGUILayout.HelpBox("Default value of 1 (Not stackable item)", MessageType.Info);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif