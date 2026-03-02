using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class InputReader : MonoBehaviour
{
    private PlayerInputActions inputActions;

    public event Action<Vector2> OnBuildPressed;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Gameplay.Build.performed += HandleBuild;
    }

    private void OnDisable()
    {
        inputActions.Gameplay.Build.performed -= HandleBuild;
        inputActions.Disable();
    }

    private void HandleBuild(InputAction.CallbackContext context)
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        OnBuildPressed?.Invoke(mousePosition);
    }
}