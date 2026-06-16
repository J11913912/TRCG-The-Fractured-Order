using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
     #region InputActions

        private InputSystem_Actions _inputActions;
        private InputAction _moveAction;
        private InputAction _rollAction;
        private InputAction _attackAction;
        private InputAction _interactAction;

        #endregion

        private Vector2 _lasMoveInput;
        
        #region Unity Events

        private void Awake()
        {
            _inputActions = new InputSystem_Actions();
            _moveAction = _inputActions.Player.Move;
            _rollAction = _inputActions.Player.Roll;
            _attackAction = _inputActions.Player.Attack;
            _interactAction = _inputActions.Player.Interact;

        }

        private void OnEnable()
        {
            EnableInput();
            _moveAction.performed += Move;
            _moveAction.canceled += Move;

            _rollAction.performed += Roll;

            _attackAction.performed += Attack;

            _interactAction.performed += Interact;
        }

        private void OnDisable()
        {
            DisableInput();
            _moveAction.performed -= Move;
            _moveAction.canceled -= Move;

            _rollAction.performed -= Roll;
            
            _attackAction.performed -= Attack;
            
            _interactAction.performed += Interact;
        }

        public void EnableInput()
        {
            _inputActions.Enable();
        }
        
        public void DisableInput()
        {
            _inputActions.Disable();
        }

        #endregion

        #region InputMethods

        private void Move(InputAction.CallbackContext ctx)
        {
            Vector2 newInput = ctx.ReadValue<Vector2>();
            if (_lasMoveInput != newInput)
            {
                float xValue = Mathf.Abs(_lasMoveInput.x - newInput.x);
                float yValue = Mathf.Abs(_lasMoveInput.y - newInput.y);

                if (xValue > yValue)
                {
                    PlayerStates.OnHorizontalChangeDirection?.Invoke(newInput.x);
                }
                else if(xValue < yValue)
                {
                    PlayerStates.OnVerticalChangeDirection?.Invoke(newInput.y);
                }
                else
                {
                    PlayerStates.OnChangeDirection?.Invoke(newInput);
                }
            }

            PlayerController.OnMoveInput?.Invoke(ctx.ReadValue<Vector2>());

            _lasMoveInput = ctx.ReadValue<Vector2>();
        }
        
        private void Roll(InputAction.CallbackContext ctx)
        {
            //PlayerRoll.OnRollInput?.Invoke();
        }

        private void Attack(InputAction.CallbackContext ctx)
        {
            //PlayerAttack.OnAttackInput?.Invoke();
        }

        private void Interact(InputAction.CallbackContext ctx)
        {
            //PlayerInteraction.OnInteract?.Invoke();
        }

        #endregion
}
