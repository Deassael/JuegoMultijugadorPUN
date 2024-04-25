using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    private PlayerInput _playerInput;
    
    private InputAction _moveAction;
    private InputAction _cameraMoveAction;
    
    public float moveSpeed = 5f;
    
    public LayerMask groundMask;
    
    public Transform cameraTransform;
    public float cameraSpeed = 2f;
    public Transform cameraCapsule;
    
    private Rigidbody _rigidbody;
    private Vector2 _movementInput;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        
        _playerInput = GetComponent<PlayerInput>();
        
        _moveAction = _playerInput.actions.FindAction("Move");
        _cameraMoveAction = _playerInput.actions.FindAction("Look");
        
        _moveAction.performed += contexto => _movementInput = contexto.ReadValue<Vector2>();
        _moveAction.canceled += contexto => _movementInput = Vector2.zero;
        
        _cameraMoveAction.performed += contexto => MoveCamera(contexto.ReadValue<Vector2>());
    }
    
    void FixedUpdate()
    {
        Vector3 moveDirection = new Vector3(_movementInput.x, 0f,_movementInput.y).normalized;
        Vector3 moveVelocity = moveDirection * moveSpeed;
        _rigidbody.velocity = new Vector3(moveVelocity.x, _rigidbody.velocity.y, moveVelocity.z);
    }
    
    void MoveCamera(Vector2 direction)
    {
        cameraCapsule.Rotate(Vector3.up, direction.x * cameraSpeed * Time.deltaTime, Space.World);
        
        cameraTransform.Rotate(Vector3.right, -direction.y * cameraSpeed * Time.deltaTime, Space.Self);
    }
    
    void OnEnable()
    {
        _moveAction.Enable();
        _cameraMoveAction.Enable();
    }
    void OnDisable()
    {
        _moveAction.Disable();
        _cameraMoveAction.Disable();
    }


}
