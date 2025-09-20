using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlotItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("UI")]
    public Image image;

    [HideInInspector] public ItemData itemData;
    [HideInInspector] public Transform parentAfterDrag;

    public void Start()
    {
        InitializeItem(itemData);
    }

    private void InitializeItem(ItemData newItem)
    {
        itemData = newItem;
        image.sprite = newItem.icon;
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
    }
}
