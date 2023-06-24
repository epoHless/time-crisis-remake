using UnityEngine;

public class BulletParticle : MonoBehaviour
{
    private ParticleSystem particles;
    private ParticleSystem.MainModule emittersModule;
    
    #region Unity Methods

    private void Awake()
    {
        particles = GetComponent<ParticleSystem>();
        emittersModule = particles.main;
    }
    
    protected virtual void OnEnable()
    {
        emittersModule.stopAction = ParticleSystemStopAction.Callback;
        particles.Play(true);
    }

    protected virtual void OnDisable()
    {
        emittersModule.stopAction = ParticleSystemStopAction.None;
    }

    private void OnParticleSystemStopped()
    {
        gameObject.SetActive(false);
    }

    #endregion
}
