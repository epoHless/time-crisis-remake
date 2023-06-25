using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
public class CoverComponent : MonoBehaviour
{
    [SerializeField] private GameObject camera;
    [SerializeField] private CapsuleCollider collider;

    private void OnEnable()
    {
        InputManager.Player.Cover.performed += Cover;
        InputManager.Player.Cover.canceled += Uncover;
        
        EventManager.OnCheckpointCleared.AddListener(DisableCover);
        EventManager.OnCheckpointStart.AddListener(EnableCover);
    }

    private void OnDisable()
    {
        InputManager.Player.Cover.performed -= Cover;
        InputManager.Player.Cover.canceled -= Uncover;
        
        EventManager.OnCheckpointCleared.RemoveListener(DisableCover);
        EventManager.OnCheckpointStart.RemoveListener(EnableCover);
    }

    private void Uncover(InputAction.CallbackContext obj)
    {
        InputManager.ToggleShoot(true);

        camera.transform.DOMoveY(1, 0.25f);
        collider.enabled = true;
    }

    private void Cover(InputAction.CallbackContext obj)
    {
        InputManager.ToggleShoot(false);
        
        camera.transform.DOMoveY(0, 0.25f);
        collider.enabled = false;
    }
    
    private void DisableCover()
    {
        InputManager.ToggleCover(false);
    }
    
    private void EnableCover()
    {
        InputManager.ToggleCover(true);
    }
}