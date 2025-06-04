using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rb2d;

        private Vector2 _movement;

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
            _rb2d.velocity = _movement * _speed;

            Vector2 velocity = _rb2d.velocity;

            bool isMoving = velocity.magnitude > 0.1f;
            _animator.SetBool("isMoving", isMoving);

            if (isMoving)
            {
                // Use raw input for blend tree values, clamped between -1 and 1
                Vector2 inputDir = _movement.normalized;

                _animator.SetFloat("moveX", inputDir.x);
                _animator.SetFloat("moveY", inputDir.y);

                // Flip sprite based on horizontal input (not velocity)
                if (_movement.x > 0.1f)
                {
                    _spriteRenderer.flipX = true;
                }
                else if (_movement.x < -0.1f)
                {
                    _spriteRenderer.flipX = false;
                }
            }

        }

        private void GetComponents()
        {
            _rb2d = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();

            if (_rb2d == null) Debug.LogError("RigidBody is empty");
            if (_animator == null) Debug.LogError("Animator is empty");
            if (_spriteRenderer == null) Debug.LogError("SpriteRenderer is empty");
        }
    }
}
