using Code.Input;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClickController : MonoBehaviour
{
    private NSFPInput _input;
    public Action<Vector2> OnMouseLeftButtonClick;
    public Action OnMouseRightButtonClick;
    public static Action OnEscapeButtonPress;

    private void Awake()
    {
        _input = new NSFPInput();
    }

    void Start()
    {
        _input.Gameplay.Enable();
        _input.Gameplay.LeftClick.performed += OnClickLeftButton;
        _input.Gameplay.RightClick.performed += OnClickRightButton;
        _input.Gameplay.PauseMenu.performed += OnEscapeButton;
    }

    private void OnDisable()
    {
        _input.Gameplay.LeftClick.performed -= OnClickLeftButton;
        _input.Gameplay.RightClick.performed -= OnClickRightButton;
        _input.Gameplay.PauseMenu.performed -= OnEscapeButton;
        _input.Gameplay.Disable();
    }

    private void OnClickLeftButton(InputAction.CallbackContext context)
    {
        var mousePosition = Mouse.current.position.ReadValue();
        OnMouseLeftButtonClick?.Invoke(mousePosition);
    }

    private void OnClickRightButton(InputAction.CallbackContext context)
    {
        OnMouseRightButtonClick?.Invoke();
    }
    private void OnEscapeButton(InputAction.CallbackContext context)
    {
        if(_input.Gameplay.PauseMenu.IsPressed())
            OnEscapeButtonPress?.Invoke();
    }
}
