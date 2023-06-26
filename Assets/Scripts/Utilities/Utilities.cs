using UnityEngine;

public static class Utilities
{
    public static Transform PlayerTransform;
    private static bool _isInit;
    
    static Utilities()
    {
        if (_isInit) return;

        PlayerTransform = GameObject.FindObjectOfType<HealthComponent>().transform;
        _isInit = true;
    }
}
