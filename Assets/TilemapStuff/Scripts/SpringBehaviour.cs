using System;
using UnityEngine;

public class SpringBehaviour : MonoBehaviour
{
    public Vector2 direction = Vector2.up;
    public float force = 400f;
    
    private Animator _animator;
    private static readonly int Active = Animator.StringToHash("active");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Rigidbody2D rb = other.rigidbody;
        
        if (!(rb is null))
            rb.AddForce(direction * force);
        
        if (!(_animator is null))
            _animator.SetTrigger(Active);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        // Do nothing
    }
}
