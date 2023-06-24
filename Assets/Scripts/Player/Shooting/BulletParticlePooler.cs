using MobileFramework.ObjectPooler;
using UnityEngine;

public class BulletParticlePooler : ObjectPooler<BulletParticlePooler>
{
    protected override void OnEnable()
    {
        base.OnEnable();
        EventManager.OnBulletHit.AddListener(OnBulletHit);
    }

    private void OnDisable()
    {
        EventManager.OnBulletHit.RemoveListener(OnBulletHit);
    }

    private void OnBulletHit(Vector3 _position)
    {
        var particle = GetPooledObject<GameObject>();
        particle.transform.position = _position;
        particle.SetActive(true);
    }
}