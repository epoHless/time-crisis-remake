using UnityEditor;
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

#if UNITY_EDITOR
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
#endif
    private static void Init()
    {
        if (!_isInit)
        {
            _inputActions ??= new PlayerInputActions();
            _isInit = true;

            EventManager.OnGameStart.AddListener(() =>
            {
                _inputActions.Enable();
                ToggleInputs(true);
            });
            
            EventManager.OnGameOver.AddListener(s => { ToggleInputs(false); });
        }
    }

    private static void ToggleInputs(bool _value)
    {
        ToggleShoot(_value);
        ToggleCover(_value);
        ToggleReload(_value);
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
