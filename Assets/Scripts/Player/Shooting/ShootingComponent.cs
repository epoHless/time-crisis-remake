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
        InputManager.Player.Shoot.started += Shoot;
        InputManager.Player.Reload.started += Reload;
        
        EventManager.OnGameStart.AddListener(OnStart);
    }

    private void OnDisable()
    {
        InputManager.Player.Shoot.started -= Shoot;
        InputManager.Player.Reload.started -= Reload;
        
        EventManager.OnGameStart.RemoveListener(OnStart);
    }

    private void Start()
    {
        currentAmmo = capacity;
    }

    #endregion

    #region Event Methods
    
    private void OnStart()
    {
        Reload();
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
                EventManager.OnBulletHitEntity?.Invoke();
            }
            
            EventManager.OnBulletHit?.Invoke(hit.point);
        }

        if (currentAmmo == 0)
        {
            Reload();
        }
    }

    private void Reload(InputAction.CallbackContext obj)
    {
        Reload();
    }
    
    private void Reload()
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
