using System;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInputMap _playerInputMap;

    private void Awake()
    {
        _playerInputMap = new PlayerInputMap();
    }

    private void OnEnable()
    {
        _playerInputMap.Enable();
    }

    private void Update()
    {
        HandleMouseInput();
    }

    private void HandleMouseInput()
    {
        if (!_playerInputMap.Player.Interact.triggered) return;
        
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(!Physics.Raycast(ray, out var raycastHit, float.MaxValue)) return;
        
        if (!raycastHit.transform.TryGetComponent(out IInteractable interactable)) return;
        interactable.Interact();
    }

    private void OnDisable()
    {
        _playerInputMap.Disable();
    }
}
