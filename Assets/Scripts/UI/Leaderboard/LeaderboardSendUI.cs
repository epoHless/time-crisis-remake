using System;
using System.Collections;
using DG.Tweening;
using PlayFab;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardSendUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Button btn_send;
    [SerializeField] private Button btn_cancel;
    [SerializeField] private Button btn_open;

    [SerializeField] private TMP_Text result;

    private float finalTime;
    private RectTransform rectTransform;

    private bool sent;
    
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        btn_cancel.onClick.AddListener(ClosePanel);
        btn_open.onClick.AddListener(OpenPanel);
        btn_send.onClick.AddListener(SendData);
        
        EventManager.OnFinalTimeRequested.AddListener(StoreTime);
        EventManager.OnUsernameSet.AddListener(SendData);
        
        EventManager.OnLeaderboardUpdated.AddListener(SendResult);
    }

    private void OnDisable()
    {
        btn_cancel.onClick.RemoveListener(ClosePanel);
        btn_open.onClick.RemoveListener(OpenPanel);
        btn_send.onClick.RemoveListener(SendData);
        
        EventManager.OnFinalTimeRequested.RemoveListener(StoreTime);
        EventManager.OnUsernameSet.RemoveListener(SendData);
        
        EventManager.OnLeaderboardUpdated.RemoveListener(SendResult);
    }

    private void StoreTime(TimerTick _timer)
    {
        finalTime = _timer.Seconds;
    }

    private void SendData()
    {
        if (inputField.text == "" || sent) return;
        PlayfabManager.SetUsername(inputField.text);
    }
    
    private void SendData(string obj)
    {
        PlayfabManager.SendLeaderboard((int)finalTime);

        ClosePanel();
        sent = true;
    }

    private void SendResult(string obj)
    {
        result.text = $"Your score has been registered!";
        result.DOFade(1, 0.5f).SetLoops(2, LoopType.Yoyo);
    }
    
    private void OpenPanel()
    {
        rectTransform.DOAnchorPosX(-550, .5f)
            .SetEase(Ease.OutBack);
    }
    
    private void ClosePanel()
    {
        rectTransform.DOAnchorPosX(0, .5f)
            .SetEase(Ease.InBack);
    }
}
