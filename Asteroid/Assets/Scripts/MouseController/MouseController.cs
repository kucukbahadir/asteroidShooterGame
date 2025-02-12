using System;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    [SerializeField] private GameObject mouseControllerDebugPrefab;
    [SerializeField] private bool useDebugPrefab;

    private GameObject currentMouseControllerPrefab;
    
    private void Start()
    {
        if (!useDebugPrefab) return;

        currentMouseControllerPrefab = Instantiate(mouseControllerDebugPrefab);
    }

    private void Update ()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(!Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue)) return;

        OnUsingDebugPrefab(raycastHit);

        if (Input.GetMouseButtonDown(0))
        {
            if (!raycastHit.transform.TryGetComponent(out IInteractable interactable)) return;
            interactable.Interact();   
        }
    }

    private void OnUsingDebugPrefab(RaycastHit raycastHit)
    {
        if (!useDebugPrefab) return;
        currentMouseControllerPrefab.transform.position = raycastHit.point;
    }
}
