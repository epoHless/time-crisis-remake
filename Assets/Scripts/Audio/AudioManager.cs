using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource)), DisallowMultipleComponent]
public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip bulletFiredClip;
    [SerializeField] private AudioClip reloadClip;
    [SerializeField] private AudioClip explosionSound;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        EventManager.OnBulletFired.AddListener(PlayBulletSound);
        EventManager.OnReload.AddListener(PlayReloadSound);
        
        EventManager.OnBulletRequested.AddListener(PlayBulletSound);
        EventManager.OnExplosion.AddListener(PlayExplosionSound);
    }

    private void OnDisable()
    {
        EventManager.OnBulletFired.RemoveListener(PlayBulletSound);
        EventManager.OnReload.RemoveListener(PlayReloadSound);
        
        EventManager.OnBulletRequested.RemoveListener(PlayBulletSound);
        EventManager.OnExplosion.RemoveListener(PlayExplosionSound);
    }

    private void PlayExplosionSound()
    {
        audioSource.PlayOneShot(explosionSound);
    }

    private void PlayBulletSound(Transform obj)
    {
        audioSource.PlayOneShot(bulletFiredClip);
    }

    private void PlayReloadSound()
    {
        audioSource.PlayOneShot(reloadClip);
    }

    private void PlayBulletSound(int obj)
    {
        audioSource.PlayOneShot(bulletFiredClip);
    }
}
