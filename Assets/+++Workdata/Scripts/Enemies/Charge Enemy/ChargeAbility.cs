using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class ChargeAbility : MonoBehaviour
{
    public static Action<Vector2> onChargeEnter;
    public static Action<bool> onTargetChange;
    
    private Vector2 chargeTarget;
    private Vector2 chargeTargetBeyond;
    private Vector2 _currentPosition;
    private Vector2 _direction;

    private Rigidbody2D _rb;

    public float force;

    public bool chargingAllowed;
    public bool currentlyCharging;
   // public bool canCharge;
    private bool targetInReach = false;

    private int _enemyLayer;
    private int _playerLayer;

    
    private InputSystem_Actions _inputActions;
    private InputAction _clickAction;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

        _enemyLayer = LayerMask.NameToLayer("Enemy");
        _playerLayer = LayerMask.NameToLayer("Default");
        
        
        _inputActions = new InputSystem_Actions();

        _clickAction = _inputActions.Player.ClickTest;

    }

    private void OnEnable()
    {
        onChargeEnter += SetTarget;
        onTargetChange += SetTargetInReach;
        
        _inputActions.Enable();
        _clickAction.performed += ClickTest;
    }

    private void OnDisable()
    {
        onChargeEnter -= SetTarget;
        onTargetChange -= SetTargetInReach;
        
        _inputActions.Disable();
        _clickAction.performed -= ClickTest;

    }

    private void FixedUpdate()
    {
        if (chargingAllowed)
        {
            if (!targetInReach)
            {
                StopCharge();
                return;
            }
            
            Charge();
        }
    }

    private void ClickTest(InputAction.CallbackContext context)
    {
        GetTarget();
    }
    
    public void GetTarget()
    {
        if (currentlyCharging) return;
        
        TransferTarget.onChargeStart?.Invoke();
    }

    public void SetTarget(Vector2 target)
    { 
        chargingAllowed = true;
        
        chargeTarget = target;
        
        _currentPosition = transform.position;
        
        _direction = chargeTarget - _currentPosition;
        _direction.Normalize();
        
        chargeTargetBeyond = chargeTarget + _direction * 5f;
        
        Physics2D.IgnoreLayerCollision(_enemyLayer, _playerLayer, true);

    }

    public void SetTargetInReach(bool _targetInReach)
    {
        targetInReach = _targetInReach;
    }
    

    private void Charge()
    {
        float distance = Vector2.Distance(transform.position, chargeTargetBeyond);
        
        Debug.Log(distance);
        
        if (distance > 0.1f)
        {
            _rb.linearVelocity = _direction * force;
            currentlyCharging = true;
        }
        else
        {
            StopCharge();
        }
    }

    public void StopCharge()
    {
        _rb.linearVelocity = Vector2.zero;
        
        chargingAllowed = false;
        currentlyCharging = false;
        
        Physics2D.IgnoreLayerCollision(_enemyLayer, _playerLayer, false);
    }
}

