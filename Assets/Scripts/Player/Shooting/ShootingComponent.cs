using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingComponent : MonoBehaviour
{
    #region Fields

    [SerializeField] private int capacity;
    private int currentAmmo;

    private Camera cam;
    
    #endregion

    #region Unity Methods

    private void Awake()
    {
        cam = Camera.main;
    }

    private void OnEnable()
    {
        InputManager.Player.Shoot.performed += Shoot;
        InputManager.Player.Reload.performed += Reload;
        
        EventManager.OnCheckpointCleared.AddListener(OnCheckpointCleared);
        EventManager.OnCheckpointStart.AddListener(OnCheckpointStart);
    }

    private void OnDisable()
    {
        InputManager.Player.Shoot.performed -= Shoot;
        InputManager.Player.Reload.performed -= Reload;
        
        EventManager.OnCheckpointCleared.RemoveListener(OnCheckpointCleared);
        EventManager.OnCheckpointStart.RemoveListener(OnCheckpointStart);
    }

    private void Start()
    {
        currentAmmo = capacity;
    }

    #endregion

    #region Event Methods

    private void OnCheckpointCleared()
    {
        InputManager.ToggleShoot(false);
        InputManager.ToggleReload(false);
    }
    
    private void OnCheckpointStart()
    {
        InputManager.ToggleShoot(true);
        InputManager.ToggleReload(true);
    }
    
    private void Shoot(InputAction.CallbackContext obj)
    {
        currentAmmo--;
        
        EventManager.OnBulletFired?.Invoke(currentAmmo);
        
        Ray ray = cam.ScreenPointToRay(InputManager.MousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(1);
            }
            
            EventManager.OnBulletHit?.Invoke(hit.point);
        }

        if (currentAmmo == 0)
        {
            Reload(obj);
        }
    }

    private void Reload(InputAction.CallbackContext obj)
    {
        InputManager.ToggleShoot(false);

        currentAmmo = capacity;
        
        var sequence = DOTween.Sequence().AppendInterval(.35f);

        sequence.onComplete += () =>
        {
            InputManager.ToggleShoot(true);
            EventManager.OnReload?.Invoke();
        };
    }

    #endregion
}
