using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneSystem
{
    #if !UNITY_EDITOR
    
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void LoadEnvironment()
    {
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive).completed += operation =>
        {
            operation.completed += asyncOperation =>
            {
                asyncOperation.completed += operation1 =>
                {
                    Utilities.Init();
                };
            };
        };
    }
    
    #endif
}
