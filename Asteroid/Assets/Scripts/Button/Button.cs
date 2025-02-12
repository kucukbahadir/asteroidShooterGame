using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour, IInteractable
{
    public UnityEvent OnButtonInteract;
    
    public void Interact()
    {
        OnButtonInteract?.Invoke();
    }
}
