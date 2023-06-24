using System.Collections;
using UnityEngine;

public class BulletParticle : MonoBehaviour
{
    private ParticleSystem particleSystem;
    private ParticleSystem.MainModule emittersModule;
    
    #region Unity Methods

    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
        emittersModule = particleSystem.main;
    }
    
    protected virtual void OnEnable()
    {
        emittersModule.stopAction = ParticleSystemStopAction.Callback;
        particleSystem.Play(true);
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
