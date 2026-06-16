using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static Action<Vector2> OnMoveInput;
        public static Action<float> OnForceApply;

        #region Inspector

        [Header("Movement Settings")] 
        [SerializeField] private float walkingSpeed = 5;

        [SerializeField] private float acceleration = 10f;

        #endregion

        #region Private Variables

        private PlayerStates _playerState;
        
        private Rigidbody2D _rb;
        public Rigidbody2D Rb => _rb;

        private Vector2 _moveInput;
        public Vector2 MoveInput => _moveInput;

        private float _currentSpeed;

        public bool _isRolling = false;

        #endregion

        #region Unity Events

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _playerState = GetComponent<PlayerStates>();
            
            _currentSpeed = walkingSpeed;
        }

        private void OnEnable()
        {
            OnMoveInput += SetMoveInput;
            OnForceApply += ApplyForce;
        }

        private void FixedUpdate()
        {
            MoveHandler();
        }

        private void OnDisable()
        {
            OnMoveInput -= SetMoveInput;
            OnForceApply -= ApplyForce;
        }

        #endregion

        #region Handler Methods

        void MoveHandler()
        {
            //if (_playerState.GetPlayerAction() == PlayerAction.Roll) return;
            
            Vector2 targetVelocity = _moveInput * _currentSpeed;
            Vector2 currentVelocity = _rb.linearVelocity;

            _rb.linearVelocity = Vector2.Lerp(currentVelocity, targetVelocity, Time.fixedTime * acceleration);
        }

        void SetMoveInput(Vector2 moveInput)
        {
            _moveInput = moveInput;

            PlayerStates.OnChangeMovement?.Invoke(_moveInput == Vector2.zero ? PlayerMovement.Idle : PlayerMovement.Moving);
            
            
            if (_moveInput.x < 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (_moveInput.x > 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }

        #endregion

        #region Physics

        void ApplyForce(float force)
        {
            _rb.AddForce(_moveInput * force, ForceMode2D.Impulse);

            if (_moveInput.x == 0 && _moveInput.y == 0 && _playerState.GetPlayerDirection() == PlayerDirection.Right)
            {
                _rb.AddForce(Vector2.right * force, ForceMode2D.Impulse);
            }
            
            else if (_playerState.GetPlayerDirection() == PlayerDirection.Left)
            {
                _rb.AddForce(Vector2.left * force, ForceMode2D.Impulse);
            }
            
            else if (_playerState.GetPlayerDirection() == PlayerDirection.Up)
            {
                _rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
            }
            
            else if (_playerState.GetPlayerDirection() == PlayerDirection.Down)
            {
                _rb.AddForce(Vector2.down * force, ForceMode2D.Impulse);
            }
        }

        #endregion
}
