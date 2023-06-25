using UnityEngine;
using UnityEngine.InputSystem;

public static class InputManager
{
    private static PlayerInputActions _inputActions;
    private static bool _isInit;

    public static PlayerInputActions.PlayerActions Player => _inputActions.Player;
    public static PlayerInputActions.UIActions UI => _inputActions.UI;

    public static Vector2 MousePosition => Mouse.current.position.ReadValue();
    
    static InputManager()
    {
        Init();
    }

    private static void Init()
    {
        if (_isInit) return;
        
        _inputActions ??= new PlayerInputActions();
        _inputActions.Enable();
        _isInit = true;
    }

    public static void ToggleShoot(bool _value)
    {
        if (_value)
            Player.Shoot.Enable();
        else 
            Player.Shoot.Disable();
    }
    
    public static void ToggleReload(bool _value)
    {
        if (_value)
            Player.Reload.Enable();
        else 
            Player.Reload.Disable();
    }
    
    public static void ToggleCover(bool _value)
    {
        if (_value)
            Player.Cover.Enable();
        else 
            Player.Cover.Disable();
    }
}
