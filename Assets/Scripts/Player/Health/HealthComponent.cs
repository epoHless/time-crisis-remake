using UnityEngine;

public class HealthComponent : MonoBehaviour, IDamageable
{
    [SerializeField] private int health;
    [field: SerializeField] public CapsuleCollider Collider { get; set; }

    public int Health
    {
        get => health;
        set
        {
            health = value;
            EventManager.OnDamageTaken?.Invoke(health);
        }
    }

    public void OnDeath()
    {
        EventManager.OnGameOver?.Invoke("failed");
    }
}
