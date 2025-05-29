using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace interact {

    public class Interactor : MonoBehaviour
    {

        private bool _isInteracted = false;
        [SerializeField] private float _interactionDistance = 3f;
        [SerializeField] private LayerMask _interactableLayer;

        private Vector2 _lastInputDirection = Vector2.up;

        private void Update()
        {
            Vector2 origin = transform.position;
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if(input != Vector2.zero) {
                _lastInputDirection = input.normalized;
            }

            if(Input.GetKeyDown(KeyCode.E) && !_isInteracted) {

                StartCoroutine(DelayedInteract());

                Vector2 direction = _lastInputDirection;

                RaycastHit2D hit = Physics2D.Raycast(origin, direction, _interactionDistance, _interactableLayer);

                if (hit.collider != null)
                {
                    IInteractible interactable = hit.collider.GetComponent<IInteractible>();
                    if (interactable != null)
                    {
                        interactable.OnInteract();
                    }
                }

                Debug.DrawRay(origin, direction * _interactionDistance, Color.red, 0.2f);
            }   
        }


        private IEnumerator DelayedInteract() {
            _isInteracted = true;
            yield return new WaitForSeconds(0.5f);
            _isInteracted = false;
        }
    }
}

