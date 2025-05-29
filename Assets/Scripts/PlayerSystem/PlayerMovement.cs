using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {

    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;
        private float _maxSpeed;

        private Vector2 _movement;

        private Rigidbody2D _rb2d;

        private void Start()
        {
            _rb2d = GetComponent<Rigidbody2D>();
            if(_rb2d == null) {
                Debug.Log("Rigidbody is empty!!!");
            }

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
    }
}
