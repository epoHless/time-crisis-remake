using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BulletsUI : MonoBehaviour
{
    [SerializeField] private List<RectTransform> bullets = new List<RectTransform>();
    private List<GameObject> bulletObjects = new List<GameObject>();

    private void Awake()
    {
        foreach (var bullet in bullets)
        {
            var child = bullet.transform.GetChild(0).gameObject;
            bulletObjects.Add(child);
        }
    }

    private void OnEnable()
    {
        EventManager.OnBulletFired.AddListener(OnBulletFired);
        EventManager.OnReload.AddListener(OnReload);
    }

    private void OnDisable()
    {
        EventManager.OnBulletFired.RemoveListener(OnBulletFired);
        EventManager.OnReload.RemoveListener(OnReload);
    }

    private void OnReload()
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            bulletObjects[i].transform.localPosition = Vector3.zero;
            bulletObjects[i].transform.localRotation = Quaternion.identity;
            bulletObjects[i].transform.localScale = Vector3.one * 200;

            bullets[i].gameObject.SetActive(true);
        }
    }

    private void OnBulletFired(int _value)
    {
        var endPos = new Vector3(50, 50);

        bulletObjects[_value].transform.DOLocalRotate(Vector3.forward * -45, 0.3f).SetEase(Ease.OutBack);
        bulletObjects[_value].transform.DOLocalMove(endPos, 0.15f).SetEase(Ease.OutBack).onComplete += () =>
        {
            bulletObjects[_value].transform.DOScale(Vector3.zero, 0.15f).SetEase(Ease.OutBack).onComplete += () =>
            {
                bullets[_value].gameObject.SetActive(false);
            };
        };
    }
}
