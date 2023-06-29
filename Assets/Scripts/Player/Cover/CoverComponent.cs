using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
public class CoverComponent : MonoBehaviour
{
    #region Fields

    [SerializeField] private GameObject camera;
    [SerializeField] private CapsuleCollider collider;

    private Vector3 peekDirection;
    private Vector3 position;
    
    #endregion

    #region Unity Events

    private void OnEnable()
    {
        InputManager.Player.Cover.started += Cover;
        InputManager.Player.Cover.canceled += Uncover;
        
        EventManager.OnPeekChanged.AddListener(ChangePeek);
    }

    private void OnDisable()
    {
        InputManager.Player.Cover.started -= Cover;
        InputManager.Player.Cover.canceled -= Uncover;
        
        EventManager.OnPeekChanged.RemoveListener(ChangePeek);
    }

    #endregion

    #region Event Methods

    private void Uncover(InputAction.CallbackContext obj)
    {
        InputManager.ToggleShoot(true);

        camera.transform.DOLocalMove(position, 0.25f); 
        collider.enabled = true;
    }

    private void Cover(InputAction.CallbackContext obj)
    {
        InputManager.ToggleShoot(false);
        
        camera.transform.DOLocalMove(peekDirection, 0.25f);
        collider.enabled = false;
    }
    
    private void ChangePeek(Vector3 _direction)
    {
        peekDirection = _direction;
        position = transform.localPosition;
    }

    #endregion
}