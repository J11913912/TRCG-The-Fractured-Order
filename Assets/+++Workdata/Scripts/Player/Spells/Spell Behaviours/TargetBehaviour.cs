using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class TargetBehaviour : MonoBehaviour
{
    public static Action BasicAoE;

    public float moveSpeed;
    private float _defaultMoveSpeed;

    public float timeToSelfDestruct = 20f;
    private float timer;

    private bool _castStarted = false;

    private InputSystem_Actions _inputActions;
    private InputAction _moveAction;
    
    private Vector2 _moveInput;
    
    private Rigidbody2D _rb;

    private GameObject _player;

    private void Awake()
    {
        _inputActions = new InputSystem_Actions();
        _moveAction = _inputActions.Player.MoveForTarget;
        
        _rb = GetComponent<Rigidbody2D>();

        _defaultMoveSpeed = moveSpeed;
        
        _player = GameObject.Find("Player");
    }

    private void OnEnable()
    {
        _inputActions.Enable();
        _moveAction.performed += Move;
        _moveAction.canceled += Move;
    }

    private void OnDisable()
    {
        _inputActions.Disable();
        _moveAction.performed -= Move;
        _moveAction.canceled -= Move;
    }

    private void FixedUpdate()
    {
        _rb.linearVelocity = _moveInput * moveSpeed;                                                                    // movement
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeToSelfDestruct)
        {
            _player.GetComponent<CrystalAoESpell>().Attack2Backup();
        }
    }

    private void Move(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    public void ToggleMoveSpeed(bool value)                                                                             // movement speed
    {
        if (value)
        {
            moveSpeed = _defaultMoveSpeed;
        }
        else if (!value)
        {
            moveSpeed = 1f;
        }
    }
}