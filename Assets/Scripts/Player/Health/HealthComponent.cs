using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour, IDamageable
{
    [SerializeField] private int health;
    [field: SerializeField] public CapsuleCollider Collider { get; set; }

    private bool _isInvulnerable;
    
    public int Health
    {
        get => health;
        set
        {
            if (_isInvulnerable) return;
            
            health = value;
            EventManager.OnDamageTaken?.Invoke(health);
        }
    }

    private void OnEnable()
    {
        EventManager.OnCheckpointCleared.AddListener(OnCheckpointCleared);
        EventManager.OnCheckpointReached.AddListener(OnCheckpointReached);
    }

    private void OnDisable()
    {
        EventManager.OnCheckpointCleared.RemoveListener(OnCheckpointCleared);
        EventManager.OnCheckpointReached.RemoveListener(OnCheckpointReached);
    }

    private void OnCheckpointReached(Vector3 obj)
    {
        _isInvulnerable = false;
    }

    private void OnCheckpointCleared()
    {
        _isInvulnerable = true;
    }

    public void OnDeath()
    {
        EventManager.OnGameOver?.Invoke("failed");
    }
}
