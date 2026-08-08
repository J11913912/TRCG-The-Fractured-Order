using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public static Action<SpellDefinition, string, SpellDefinition> OnChangeBinding;
    
     #region InputActions
     
     public List<InputAction> actions = new List<InputAction>();
     public List<List<InputAction>> inputActions = new List<List<InputAction>>();
     
        private InputSystem_Actions _inputActions;
        private InputAction _moveAction;
        private InputAction _teleAction;
        
        private InputAction _baseProjectileAction;
        private InputAction _baseAoEAction;
        private InputAction _baseShieldAction;
        private InputAction _baseHealingAction;
        
        private InputAction _crystalProjectileAction;
        private InputAction _crystalAoEAction;
        private InputAction _crystalShieldAction;
        private InputAction _crystalHealingAction;
        
        private InputAction _interactAction;

        #endregion

        private Vector2 _lasMoveInput;
        
        #region Unity Events

        private void Awake()
        {
            _inputActions = new InputSystem_Actions();
            _moveAction = _inputActions.Player.Move;
            _teleAction = _inputActions.Player.Roll;
            
            _baseProjectileAction = _inputActions.Player.BaseProjectile;
            _baseAoEAction = _inputActions.Player.BaseAoE;
            _baseShieldAction = _inputActions.Player.BaseShield;
            _baseHealingAction = _inputActions.Player.BaseHealing;
            
            _crystalProjectileAction = _inputActions.Player.CrystalProjectile;
            _crystalAoEAction  = _inputActions.Player.CrystalAoE;
            _crystalShieldAction = _inputActions.Player.CrystalShield;
            _crystalHealingAction = _inputActions.Player.CrystalHealing;
            
            _interactAction = _inputActions.Player.Interact;
            
            actions.Add(_baseProjectileAction);
            actions.Add(_baseAoEAction);
            actions.Add(_baseShieldAction);
            actions.Add(_baseHealingAction);
            
            actions.Add(_crystalProjectileAction);
            actions.Add(_crystalAoEAction);
            actions.Add(_crystalShieldAction);
            actions.Add(_crystalHealingAction);
            

        }

        private void OnEnable()
        {
            EnableInput();
            _moveAction.performed += Move;
            _moveAction.canceled += Move;

            _teleAction.performed += Teleport;

            _baseProjectileAction.performed += BaseProjectile;
            _baseAoEAction.performed += BaseAoE;
            _baseShieldAction.performed += BaseShield;
            _baseHealingAction.performed += BaseHealing;
            
            _crystalProjectileAction.performed += CrystalProjectile;
            _crystalAoEAction.performed += CrystalAoE;
            _crystalShieldAction.performed += CrystalShield;
            _crystalHealingAction.performed += CrystalHealing;

            _interactAction.performed += Interact;
            
            OnChangeBinding += ChangeBinding;
        }

        private void OnDisable()
        {
            DisableInput();
            _moveAction.performed -= Move;
            _moveAction.canceled -= Move;

            _teleAction.performed -= Teleport;
            
            _baseProjectileAction.performed -= BaseProjectile;
            _baseAoEAction.performed -= BaseAoE;
            _baseShieldAction.performed -= BaseShield;
            _baseHealingAction.performed -= BaseHealing;
            
            _crystalProjectileAction.performed -= CrystalProjectile;
            _crystalAoEAction.performed -= CrystalAoE;
            _crystalShieldAction.performed -= CrystalShield;
            _crystalHealingAction.performed -= CrystalHealing;
            
            _interactAction.performed += Interact;
            
            OnChangeBinding -= ChangeBinding;
        }

        public void EnableInput()
        {
            _inputActions.Enable();
        }
        
        public void DisableInput()
        {
            _inputActions.Disable();
        }

        public void ToggleMovement(bool value)
        {
            if (value)
            {
                _moveAction.Enable();
            }
            else if (!value)
            {
                _moveAction.Disable();
            }
        }
        
        public void ToggleSpells(bool value)
        {
            if (value)
            {
                foreach (InputAction action in actions)
                {
                    action.Enable();
                }
            }
            else if (!value)
            {
                foreach (InputAction action in actions)
                {
                    action.Disable();
                }
            }
        }

        #endregion


        #region KeyBinds

        public void ChangeBinding(SpellDefinition spellDefinition, string path, SpellDefinition oldSpell)
        {
            if (oldSpell != null)
            {
                InputAction oldAction = actions[oldSpell.index];
                
                for (int i = 0; i < oldAction.bindings.Count; i++)
                {
                    InputBinding oldBinding = oldAction.bindings[i];

                    if (oldBinding.effectivePath == path)
                    {
                        oldAction.ApplyBindingOverride(i, "");
                        break;
                    }
                }
            }
            
            InputAction newAction = actions[spellDefinition.index];
                
            for (int i = 0; i < newAction.bindings.Count; i++)
            {
                InputBinding newBinding = newAction.bindings[i];

                if (string.IsNullOrEmpty(newBinding.overridePath))
                {
                    newAction.ApplyBindingOverride(i, path);
                    break;
                }
            }
            

         
            
            
          /*  if (oldSpell != null)
            {
                List<InputBinding> oldInputBindings = new List<InputBinding>();
            
                oldInputBindings = actions[oldSpell.index].bindings.ToList();

                int oldIndexBindings = 0;

                foreach (InputBinding inputBinding in oldInputBindings)
                {
                    if (inputBinding.overridePath == path)
                    {
                        InputBinding currentInputBinding = inputBinding;
                        
                        currentInputBinding.overridePath = "";
                        actions[oldSpell.index].ApplyBindingOverride(currentInputBinding); 
                    }
                
                    oldIndexBindings++;
                }
            }
            
            List<InputBinding> newInputBindings = new List<InputBinding>();
            
            newInputBindings = actions[spellDefinition.index].bindings.ToList();

            int indexBindings = 0;

            foreach (InputBinding inputBinding in newInputBindings)
            {
                Debug.Log(inputBinding.path);
                
                if (inputBinding.path == "")
                {
                    InputBinding currentInputBinding = inputBinding;
                        
                    currentInputBinding.overridePath = path;
                    actions[spellDefinition.index].ApplyBindingOverride(currentInputBinding); 
                    Debug.Log(currentInputBinding.overridePath);
                    Debug.Log(currentInputBinding.path);
                }
                
                indexBindings++;
            }
            
            */
            
            /*InputBinding currentBinding = actions[spellDefinition.index].bindings[0];
            if (currentBinding.path == "")
            {
                Debug.Log("hallloooooooooooooooo");
                actions[spellDefinition.index].ApplyBindingOverride(0, path);
            }
            else
            { 
                currentBinding = actions[spellDefinition.index].bindings[1];
                actions[spellDefinition.index].ApplyBindingOverride(1, path);
            }
            
            

         //  
            
            if (oldSpell != null)
            {
                if (actions[oldSpell.index].GetBindingIndex() == -1) return;
                
                actions[oldSpell.index].ChangeBinding(0).Erase();
                
            }
            
            //actions[spellDefinition.index].ChangeBinding(path);

         //   if (actions[spellDefinition.index].GetBindingIndexForControl())
         
         */
            
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
        
        private void Teleport(InputAction.CallbackContext ctx)
        {
            //PlayerRoll.OnRollInput?.Invoke();
        }
        
        
        #region Spells


        #region BaseSpells

        private void BaseProjectile(InputAction.CallbackContext ctx)
        {
            Debug.Log("BaseProjectile");
            BasicProjectileSpell.BaseProjectileSpell?.Invoke();
        }
        
        private void BaseAoE(InputAction.CallbackContext ctx)
        {
            Debug.Log("BaseAoE");
        }
        
        private void BaseShield(InputAction.CallbackContext ctx)
        {
            Debug.Log("BaseShield");
            BasicBubbleSpell.BaseBubbleSpell?.Invoke();
        }
        
        private void BaseHealing(InputAction.CallbackContext ctx)
        {
            Debug.Log("BaseHealing");
            BasicHealingSpell.BaseHealingSpell?.Invoke();
        }

        #endregion
        
        
        private void CrystalProjectile(InputAction.CallbackContext ctx)
        {
            Debug.Log("CrystalProjectile");
            CrystalProjectileSpell.CrysProjectileSpell?.Invoke();
        }
        
        private void CrystalAoE(InputAction.CallbackContext ctx)
        {
            Debug.Log("CrystalAoE");
            CrystalAoESpell.CrystalAoE?.Invoke();
        }
        
        private void CrystalShield(InputAction.CallbackContext ctx)
        {
            Debug.Log("CrystalShield");
        }
        
        private void CrystalHealing(InputAction.CallbackContext ctx)
        {
            Debug.Log("CrystalHealing");
        }
        
       
        
        #endregion
        

        private void Interact(InputAction.CallbackContext ctx)
        {
            Playerinteraction.OnInteract?.Invoke();
        }

        #endregion
}
