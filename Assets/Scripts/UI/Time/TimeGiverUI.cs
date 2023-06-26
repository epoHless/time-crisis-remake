﻿using System;
using DG.Tweening;
using Entities;
using TMPro;
using UnityEngine;

public class TimeGiverUI : MonoBehaviour
{
    [SerializeField] private TMP_Text time;
    [SerializeField] private Vector3 offset;
    
    private void OnEnable()
    {
        EventManager.OnTimeGiverRequested.AddListener(OnTimeGiverRequested);
    }

    private void OnDisable()
    {
        EventManager.OnTimeGiverRequested.RemoveListener(OnTimeGiverRequested);
    }

    private void OnTimeGiverRequested(float _time, Vector3 _position)
    {
        time.text = $"+{_time} sec";
        transform.position = _position + offset;
        
        time.DOFade(1, .5f).SetLoops(2, LoopType.Yoyo);
        transform.DOMoveY(transform.position.y + 1f, 1f);
    }
}
