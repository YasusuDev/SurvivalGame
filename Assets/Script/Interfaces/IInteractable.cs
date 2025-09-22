using UnityEngine;

public interface IInteractable
{
    bool OnInteract(GameObject interactor);
    
    bool OnIsActive();
}
