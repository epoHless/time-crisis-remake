using UnityEngine;

public interface IDamageable
{
    CapsuleCollider Collider { get; set; }
    public int Health { get; set; }

    public void TakeDamage(int _value)
    {
        Health--;
        if(Health == 0)
        {
            OnDeath();
            Collider.enabled = false;
        }
    }
    public void OnDeath();
}
