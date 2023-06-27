public class OpenMenuButton : MenuButton
{
    protected override void Click()
    {
        EventManager.OnMenuRequested?.Invoke();
    }
}
