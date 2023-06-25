using MobileFramework.ObjectPooler;
using UnityEngine;

public class BulletParticlePooler : ObjectPooler<BulletParticlePooler>
{
    #region Unity Methods

    protected override void OnEnable()
    {
        base.OnEnable();
        EventManager.OnBulletHit.AddListener(OnBulletHit);
    }

    private void OnDisable()
    {
        EventManager.OnBulletHit.RemoveListener(OnBulletHit);
    }

    #endregion

    #region Event Methods

    private void OnBulletHit(Vector3 _position)
    {
        var particle = GetPooledObject<GameObject>();
        particle.transform.position = _position;
        particle.SetActive(true);
    }

    #endregion
}