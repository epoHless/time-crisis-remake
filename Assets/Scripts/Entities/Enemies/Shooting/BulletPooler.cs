using MobileFramework.ObjectPooler;
using UnityEngine;

public class BulletPooler : ObjectPooler<BulletPooler>
{
    protected override void OnEnable()
    {
        base.OnEnable();
        EventManager.OnBulletRequested.AddListener(OnBulletRequested);
    }

    private void OnDisable()
    {
        EventManager.OnBulletRequested.RemoveListener(OnBulletRequested);
    }

    private void OnBulletRequested(Transform _transform)
    {
        var bullet = GetPooledObject<GameObject>();
        
        bullet.transform.position = _transform.position;
        bullet.transform.forward = _transform.forward;
        
        bullet.SetActive(true);
    }
    
    Vector3 PickShootingDirection(Vector3 muzzleForward, float spreadRadius) 
    {
        Vector3 candidate = Random.insideUnitSphere * spreadRadius + muzzleForward;
        return candidate.normalized;
    }
}
