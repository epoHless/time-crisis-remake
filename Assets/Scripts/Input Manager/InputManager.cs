public static class InputManager
{
    private static PlayerInputActions _inputActions;
    private static bool _isInit;

    public static PlayerInputActions.PlayerActions Player => _inputActions.Player;
    public static PlayerInputActions.UIActions UI => _inputActions.UI;
    
    static InputManager()
    {
        Init();
    }

    private static void Init()
    {
        if (_isInit) return;
        
        _inputActions ??= new PlayerInputActions();
        _isInit = true;
    }

    public static void ToggleShooting(bool _value)
    {
        if (_value)
            Player.Shoot.Enable();
        else 
            Player.Shoot.Disable();
    }
    
    public static void ToggleCover(bool _value)
    {
        if (_value)
            Player.Cover.Enable();
        else 
            Player.Cover.Disable();
    }
}
