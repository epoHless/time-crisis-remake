using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup)), DisallowMultipleComponent]
public class GameoverUI : MonoBehaviour
{
    [SerializeField] private TMP_Text accuracy;
    [SerializeField] private TMP_Text shots;
    [SerializeField] private TMP_Text time;
    [SerializeField] private TMP_Text info;

    [SerializeField] private Button btn_sendToLeaderboard;
    
    private CanvasGroup canvasGroup;
    
    private int shotsFired;
    private int shotsHit;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        EventManager.OnFinalTimeRequested.AddListener(UpdateData);
        EventManager.OnBulletFired.AddListener(CountFired);
        EventManager.OnBulletHit.AddListener(CountHit);
        
        EventManager.OnGameOver.AddListener(OnGameOver);
        EventManager.OnMenuRequested.AddListener(FadeOut);
    }

    private void OnDisable()
    {
        EventManager.OnFinalTimeRequested.RemoveListener(UpdateData);
        EventManager.OnBulletFired.RemoveListener(CountFired);
        EventManager.OnBulletHit.RemoveListener(CountHit);
        
        EventManager.OnGameOver.RemoveListener(OnGameOver);
    }

    private void Start()
    {
        canvasGroup.alpha = 0;
    }

    private void UpdateData(TimerTick _time)
    {
        time.text = $"{Mathf.Floor(_time.Seconds / 60).ToString("00")}:{(_time.Seconds % 60).ToString("00.00")}";
        
        shots.text = $"{shotsHit}/{shotsFired}";
        
        var acc = ((float)shotsHit)/shotsFired * 100;
        accuracy.text = $"{acc.ToString("00.0")} %";
    }
    
    private void OnGameOver(string _value)
    {
        info.text = $"area {_value}";

        bool result = _value == "completed";
        btn_sendToLeaderboard.gameObject.SetActive(result);
        
        DOTween.Sequence().AppendInterval(1f).Append(canvasGroup.DOFade(1, .25f));

        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
    }
    
    private void FadeOut()
    {
        canvasGroup.DOFade(0, .25f);
        ToggleCanvasGroup(false);
    }
    
    private void ToggleCanvasGroup(bool _value)
    {
        canvasGroup.interactable = _value;
        canvasGroup.blocksRaycasts = _value;
    }
    
    private void CountFired(int _value)
    {
        shotsFired++;
    }
    
    private void CountHit(Vector3 obj)
    {
        shotsHit++;
    }
}
