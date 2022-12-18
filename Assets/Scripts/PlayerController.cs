using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController2D))]
public class PlayerController : MonoBehaviour
{
    [field: SerializeField]
    public float walkSpeed { get; set; } = 10f;

    [field: SerializeField]
    public float gravity { get; set; } = 20f;


    private Vector2 _input;
    private Vector2 _moveDirection;
    private CharacterController2D _characterController;


    private void Start()
    {
        _characterController = gameObject.GetComponent<CharacterController2D>();
    }

    private void Update()
    {
        _moveDirection.x = _input.x;
        _moveDirection.x *= walkSpeed;

        //_moveDirection.y -= gravity * Time.deltaTime;

        _characterController.Move(_moveDirection * Time.deltaTime);

    }

    public void OnMovement(InputAction.CallbackContext context)
        => _input = context.ReadValue<Vector2>(); 
}
