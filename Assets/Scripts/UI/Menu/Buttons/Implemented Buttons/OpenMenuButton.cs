using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OpenMenuButton : MenuButton
{
    [SerializeField] private Image fade;
    
    protected override void Click()
    {
        PlayfabManager.GetLeaderboard();

        fade.DOFade(1, 0.15f).onComplete += () =>
        {
            EventManager.OnMenuRequested?.Invoke();
            DOTween.Sequence().AppendInterval(1f).onComplete += ReloadGameplay;
        };
    }

    private void ReloadGameplay()
    {
        SceneManager.UnloadSceneAsync(1).completed += operation =>
        {
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive).completed += asyncOperation =>
            {
                fade.DOFade(0, .15f);
            };
        };
    }
}
