using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
public class CharacterController2D : MonoBehaviour
{
    [field: SerializeField]
    public float raycastDistance { get; set; } = .2f;

    [field: SerializeField]
    public LayerMask layerMask { get; set; }


    [field: SerializeField]
    public bool below { get; set; }

    private Vector2 _moveAmount;
    private Vector2 _currentPosition;
    private Vector2 _lastPosition;

    private Rigidbody2D _rigidbody;
    private CapsuleCollider2D _capsuleCollider;

    private Vector2[] _raycastPosition = new Vector2[3];
    private RaycastHit2D[] _rayCastHits = new RaycastHit2D[3];

    private void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        _capsuleCollider = gameObject.GetComponent<CapsuleCollider2D>();
    }

    private void FixedUpdate()
    {
        _lastPosition = _rigidbody.position;

        _currentPosition = _lastPosition + _moveAmount;

        _rigidbody.MovePosition(_currentPosition);
        _moveAmount = Vector2.zero;

        CheckGrounded();
    }

    public void Move(Vector2 movement)
    {
        _moveAmount += movement;
    }

    private void CheckGrounded()
    {
        Vector2 raycastOrigin = _rigidbody.position - new Vector2(0, _capsuleCollider.size.y * .5f);

        _raycastPosition[0] = raycastOrigin + ((.25f * _capsuleCollider.size.x * Vector2.left) + (Vector2.up * .1f));
        _raycastPosition[1] = raycastOrigin;
        _raycastPosition[2] = raycastOrigin + ((.25f * _capsuleCollider.size.x * Vector2.right) + (Vector2.up * .1f));

        DrawDebugRays(Vector2.down, Color.green);
    }

    private void DrawDebugRays(Vector2 direction, Color color)
    {
        for (int i = 0; i < _raycastPosition.Length; i++)
        {
            Debug.DrawRay(_raycastPosition[i], direction * raycastDistance, color);
        }
    }
}
