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
    }

    private void OnDisable()
    {
        InputManager.Player.Shoot.performed -= Shoot;
    }

    private void Start()
    {
        currentAmmo = capacity;
    }

    #endregion

    #region Event Methods

    private void Shoot(InputAction.CallbackContext obj)
    {
        currentAmmo--;
        
        EventManager.OnBulletChanged?.Invoke(currentAmmo);
        
        Ray ray = cam.ScreenPointToRay(InputManager.MousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(1);
            }
            
            EventManager.OnBulletHit?.Invoke(hit.point);
        }

        if (currentAmmo <= 0)
        {
            Reload();
        }
    }

    private void Reload()
    {
        InputManager.ToggleShooting(false);

        currentAmmo = capacity;
        
        var sequence = DOTween.Sequence().AppendInterval(.5f);

        sequence.onComplete += () =>
        {
            InputManager.ToggleShooting(true);
        };
        
        EventManager.OnBulletChanged?.Invoke(currentAmmo);
    }

    #endregion
}
