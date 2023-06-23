public interface IDamageable
{
    public int Health { get; set; }

    public void TakeDamage(int _value)
    {
        Health--;
        if(Health <= 0) OnDeath();
    }
    public abstract void OnDeath();
}
