using System.Collections.Generic;
using UnityEngine;

public class BulletsUI : MonoBehaviour
{
    [SerializeField] private List<GameObject> bullets;

    private void OnEnable()
    {
        EventManager.OnBulletChanged.AddListener(OnBulletFired);
    }

    private void OnDisable()
    {
        EventManager.OnBulletChanged.RemoveListener(OnBulletFired);
    }

    private void OnBulletFired(int _value)
    {
        if(_value == 6) bullets.ForEach(o => o.SetActive(true));
        else
        {
            // var range = 6 - _value;
            
            bullets[_value].SetActive(false);
        }
    }
}
