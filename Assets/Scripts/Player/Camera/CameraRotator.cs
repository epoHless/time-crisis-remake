using DG.Tweening;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.OnCameraRotationRequested.AddListener(OnCameraRotation);
        EventManager.OnCheckpointCleared.AddListener(ResetRotation);
    }

    private void OnDisable()
    {
        EventManager.OnCameraRotationRequested.RemoveListener(OnCameraRotation);
        EventManager.OnCheckpointCleared.RemoveListener(ResetRotation);
    }

    private void OnCameraRotation(float _value)
    {
        gameObject.transform.DORotate(new Vector3(0, _value, 0), 0.5f);
    }
    
    private void ResetRotation()
    {
        gameObject.transform.DORotate(Vector3.zero, 0.5f);
    }
}
