using UnityEngine;

public abstract class Entity : MonoBehaviour, IDamageable
{
    private bool isActive;
    
    bool IsActive
    {
        set
        {
            isActive = value;
            if(isActive) Initialise();
        }
    }

    #region Methods

    public void Enable() => isActive = true;
    public void Disable() => isActive = false;

    public abstract void Initialise();
    
    #endregion
    
    #region IDamageable Implementation

    [SerializeField] private int health;

    public int Health
    {
        get => health;
        set => health = value;
    }

    public abstract void OnDeath();

    #endregion
}
