using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider)), DisallowMultipleComponent]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    
    private Rigidbody rigidbody;
    private BoxCollider collider;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<BoxCollider>();
    }

    private void OnEnable()
    {
        rigidbody.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(1);
        }
        
        gameObject.SetActive(false);
    }
}
