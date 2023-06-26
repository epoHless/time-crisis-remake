using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public static class PlayfabManager
{
    public static void Login(string _value)
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = _value,
            CreateAccount = true
        };
        
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
    }

    private static void OnError(PlayFabError obj)
    {
        EventManager.OnLoginError.Invoke($"{obj.ErrorMessage}");
    }

    private static void OnSuccess(LoginResult obj)
    {
        EventManager.OnLoginSuccess.Invoke($"{obj.PlayFabId}");
    }

    public static void SendLeaderboard(int _time)
    {
        var request = new UpdatePlayerStatisticsRequest()
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "Time",
                    Value = _time
                }
            }
        };
        
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }

    public static void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "Time Score",
            StartPosition = 0,
            MaxResultsCount = 200
        };
        
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }

    private static void OnLeaderboardGet(GetLeaderboardResult result)
    {
        List<PlayerLeaderboardEntry> leaderboard = new List<PlayerLeaderboardEntry>();

        foreach (var item in result.Leaderboard)
        {
            leaderboard.Add(item);
        }
        
        EventManager.OnLeaderboardGet.Invoke(leaderboard);
    }

    private static void OnLeaderboardUpdate(UpdatePlayerStatisticsResult obj)
    {
        EventManager.OnLeaderboardUpdated.Invoke($"Leaderboard Updated!");
    }
}
