using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup)), DisallowMultipleComponent]
public class GameoverUI : MonoBehaviour
{
    [SerializeField] private TMP_Text accuracy;
    [SerializeField] private TMP_Text shots;
    [SerializeField] private TMP_Text time;

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
    }

    private void OnDisable()
    {
        EventManager.OnFinalTimeRequested.RemoveListener(UpdateData);
        EventManager.OnBulletFired.RemoveListener(CountFired);
        EventManager.OnBulletHit.RemoveListener(CountHit);
    }

    private void Start()
    {
        canvasGroup.alpha = 0;
    }

    private void UpdateData(TimerTick _time)
    {
        time.text = $"{Mathf.Floor(_time.Seconds / 60).ToString("00")}:{(_time.Seconds % 60).ToString("00.00")}";
        shots.text = $"{shotsHit}/{shotsFired}";
        accuracy.text = $"{(shotsHit/shotsFired) * 100} %";

        canvasGroup.DOFade(1, .25f);
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
