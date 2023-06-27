using UnityEngine;

public static class Utilities
{
    public static Transform PlayerTransform
    {
        get => _playerTransform ? _playerTransform : GameObject.FindObjectOfType<HealthComponent>().transform;

        set => _playerTransform = value;
    }

    private static bool _isInit;
    private static Transform _playerTransform;

    static Utilities()
    {
        Init();
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Init()
    {
        PlayerTransform = GameObject.FindObjectOfType<HealthComponent>().transform;
        _isInit = true;
    }
}
