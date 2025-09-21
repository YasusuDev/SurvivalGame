using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlott : MonoBehaviour, IDropHandler
{
    private IDropHandler _dropHandlerImplementation;
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            InventorySlotItem inventorySlotItem = eventData.pointerDrag.GetComponent<InventorySlotItem>();
            inventorySlotItem.parentAfterDrag = transform;
        }
    }
}
