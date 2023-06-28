using DG.Tweening;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void OnEnable()
    {
        EventManager.OnDamageTaken.AddListener(OnDamageTaken);
        EventManager.OnExplosion.AddListener(OnExplosion);
    }

    private void OnDisable()
    {
        EventManager.OnDamageTaken.RemoveListener(OnDamageTaken);
        EventManager.OnExplosion.RemoveListener(OnExplosion);
    }

    private void OnDamageTaken(int obj)
    {
        cam.transform.DOShakePosition(.15f, .4f, 30);
    }
    
    private void OnExplosion()
    {
        cam.transform.DOShakePosition(.3f, 1);
    }
}
