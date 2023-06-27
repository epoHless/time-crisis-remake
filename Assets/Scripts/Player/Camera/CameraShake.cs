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
    }

    private void OnDisable()
    {
        EventManager.OnDamageTaken.RemoveListener(OnDamageTaken);
    }

    private void OnDamageTaken(int obj)
    {
        cam.transform.DOShakePosition(.15f, .4f, 30);
    }
}
