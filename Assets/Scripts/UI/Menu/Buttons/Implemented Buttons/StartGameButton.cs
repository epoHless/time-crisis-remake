public class StartGameButton : MenuButton 
{
    protected override void Click()
    {
        EventManager.OnGameStart?.Invoke();
    }
}
