using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using MobileFramework.ObjectPooler;
using PlayFab.ClientModels;
using UnityEngine;

public class LeaderboardManager : ObjectPooler<LeaderboardManager>
{
    protected override void Start()
    {
        base.Start();
        PlayfabManager.Login();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        
        EventManager.OnLoginSuccess.AddListener(OnLogin);
        EventManager.OnLeaderboardGet.AddListener(Init);
        EventManager.OnLeaderboardUpdated.AddListener(UpdateLeaderboard);
    }

    private void OnDisable()
    {
        EventManager.OnLeaderboardGet.RemoveListener(Init);
        EventManager.OnLoginSuccess.RemoveListener(OnLogin);
        EventManager.OnLeaderboardUpdated.RemoveListener(UpdateLeaderboard);
    }
    
    private void OnLogin(string obj)
    {
        PlayfabManager.GetLeaderboard();
    }

    private void Init(List<PlayerLeaderboardEntry> _leaderboardEntries)
    {
        _leaderboardEntries.Reverse();
        
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

        foreach (var leaderboardEntry in _leaderboardEntries)
        {
            var ui = GetPooledObject<GameObject>().GetComponent<LeaderboardEntryUI>();

            var score = (double)(leaderboardEntry.StatValue);
            float formattedScore = (float)score / 1000;
            var text = $"{Mathf.Floor(formattedScore / 60).ToString("00")}.{(formattedScore % 60).ToString("00.00")} time";
            ui.Init(leaderboardEntry.DisplayName, text);
            
            ui.transform.localScale = Vector3.zero;
            ui.gameObject.SetActive(true);
            ui.transform.DOScale(Vector3.one, 0.25f).SetEase(Ease.InOutBack);
        }
    }
    
    private void UpdateLeaderboard(string obj)
    {
        PlayfabManager.GetLeaderboard();
    }
}
