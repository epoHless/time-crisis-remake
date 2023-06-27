using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class MenuButton : MonoBehaviour
{
    private Button btn;

    protected virtual void Awake()
    {
        btn = GetComponent<Button>();
    }

    protected virtual void OnEnable()
    {
        btn.onClick.AddListener(Click);
    }

    protected virtual void OnDisable()
    {
        btn.onClick.RemoveListener(Click);
    }

    protected virtual void Click()
    { }
}
