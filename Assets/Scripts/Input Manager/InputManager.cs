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

    private static bool enabled;
    
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

            ToggleInputs(false);
            
            EventManager.OnCheckpointReached.AddListener(vector3 =>
            {
                _inputActions.Enable();
                enabled = true;
                Debug.Log($"Reached!");
                ToggleInputs(true);
            });
            
            EventManager.OnGameOver.AddListener(s =>
            {
                ToggleInputs(false);
                enabled = false;
            });
            
            EventManager.OnCheckpointCleared.AddListener(() =>
            {
                ToggleInputs(false);
                enabled = false;
            });
        }
    }

    private static void ToggleInputs(bool _value)
    {
        if (!enabled) return;
        
        ToggleShoot(_value);
        ToggleReload(_value);
        ToggleCover(_value);
    }

    public static void ToggleShoot(bool _value)
    {
        if (!enabled) return;

        if (_value)
            Player.Shoot.Enable();
        else 
            Player.Shoot.Disable();
    }
    
    public static void ToggleReload(bool _value)
    {
        if (!enabled) return;

        if (_value)
            Player.Reload.Enable();
        else 
            Player.Reload.Disable();
    }
    
    public static void ToggleCover(bool _value)
    {
        if (!enabled) return;

        if (_value)
            Player.Cover.Enable();
        else 
            Player.Cover.Disable();
    }
}