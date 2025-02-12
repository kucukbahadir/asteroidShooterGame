using System;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    private void Update ()
    {
        //HandleMouseInput();
    }

    private void HandleMouseInput()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(!Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue)) return;

        if (!Input.GetMouseButtonDown(0)) return;
        if (!raycastHit.transform.TryGetComponent(out IInteractable interactable)) return;
        interactable.Interact();
    }
}
