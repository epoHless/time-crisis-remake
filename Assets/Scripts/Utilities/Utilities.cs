using UnityEngine;

public static class Utilities
{
    public static Transform PlayerTransform
    {
        get
        {
            if (!_playerTransform) Init();
            return _playerTransform;
        }
        private set => _playerTransform = value;
    }

    private static Transform _playerTransform;

    static Utilities()
    {
        Init();
    }

    public static void Init()
    {
        PlayerTransform = GameObject.FindObjectOfType<HealthComponent>().transform;
    }
}
