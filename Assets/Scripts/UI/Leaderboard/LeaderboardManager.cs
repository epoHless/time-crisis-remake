using System.Collections.Generic;
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
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

        foreach (var leaderboardEntry in _leaderboardEntries)
        {
            var ui = GetPooledObject<GameObject>().GetComponent<LeaderboardEntryUI>();
            ui.Init(leaderboardEntry.DisplayName, leaderboardEntry.StatValue.ToString());
            ui.gameObject.SetActive(true);
        }
    }
    
    private void UpdateLeaderboard(string obj)
    {
        PlayfabManager.GetLeaderboard();
    }
}
