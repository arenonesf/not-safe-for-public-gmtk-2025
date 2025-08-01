using Code.Input;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClickController : MonoBehaviour
{
    private NSFPInput _input;
    public static Action<Vector2> OnMouseClicked;

    private void Awake()
    {
        _input = new NSFPInput();
    }

    void Start()
    {
        _input.Gameplay.Enable();
        _input.Gameplay.Click.performed += OnClick;
    }

    private void OnDisable()
    {
        _input.Gameplay.Click.performed -= OnClick;
        _input.Gameplay.Disable();
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        var mousePosition = Mouse.current.position.ReadValue();
        Debug.Log($"Mouse Clicked at position: {mousePosition}");
        OnMouseClicked?.Invoke(mousePosition);
    }
}
