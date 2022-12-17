using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [field: SerializeField]
    public Rigidbody2D rigidbody2D { get; set; }

    [field: SerializeField]
    public Transform groundCheck { get; set; }

    [field: SerializeField]
    public LayerMask obstacleLayer { get; set; }

    [field: SerializeField]
    public float speed { get; set; } = 5f;

    [field: SerializeField]
    public float jumpingPower { get; set; } = 15f;

    public bool isGrounded { get => Physics2D.OverlapCircle(groundCheck.position, .2f, obstacleLayer); }

    private float _horizontal;
    private bool _isFacingRight = true;


    private void Update()
    {
        rigidbody2D.velocity = new Vector2(_horizontal * speed, rigidbody2D.velocity.y);

        if (!_isFacingRight && _horizontal > 0f)
        {
            Flip();
        }
        else if (_isFacingRight && _horizontal < 0f)
        {
            Flip();
        }
        
    }


    

    private void Flip() 
    {
        _isFacingRight = !_isFacingRight;

        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public void Move(InputAction.CallbackContext context)
    {
        _horizontal = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context) 
    {
        if (context.performed && isGrounded)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpingPower);
        }

        if (context.canceled && rigidbody2D.velocity.y > 0f)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, rigidbody2D.velocity.y * .5f);
        }
    }
 }


