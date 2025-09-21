using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    
    
    [Header("Only Gameplay")]
    public ItemT type;
    public ActionType actionType;
    public Vector2Int range = new Vector2Int(5, 4);

    [Header("Only UI")] 
    public bool stackable = true;
    [Header("Only Both")]
    public Sprite image;
    

}

public enum ItemT
{
    Resource,
    Tool
}

public enum ActionType
{
    Chop,
    PickUp
    
}
