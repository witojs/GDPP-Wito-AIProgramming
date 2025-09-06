using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    
    private Rigidbody _rigidBody;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movementDirection = new Vector3(horizontal,0,vertical);
        _rigidBody.linearVelocity = movementDirection * _speed * Time.fixedDeltaTime;
    }
}
