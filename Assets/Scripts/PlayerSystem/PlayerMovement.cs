using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {

    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private Animator _animator;

        private Vector2 _movement;

        private Rigidbody2D _rb2d;

        private void Start()
        {
            GetComponents();
            Debug.Log("Game working");
        }

        private void Update()
        {
            _movement.x = Input.GetAxisRaw("Horizontal");
            _movement.y = Input.GetAxisRaw("Vertical");

        }

        private void FixedUpdate()
        {
            _rb2d.velocity = new Vector2(_movement.x * _speed, _movement.y * _speed);
        }

        private void GetComponents()
        {
            _rb2d = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            if (_rb2d == null) Debug.Log("RigidBody is empty");
            if (_animator == null) Debug.Log("Animator is empty");
        }
    }
}
