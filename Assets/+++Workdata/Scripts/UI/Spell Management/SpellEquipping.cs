using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;


public class SpellEquipping : MonoBehaviour
{
   public static Action OnFocusShift;
   public static Action<bool> OnMenuActive;
   
   private InputSystem_Actions _inputActions;
   private InputAction _arrowAction;
   private InputAction _cornerAction;
   private InputAction _focusAction;
   
   public GameObject assignModeIndicator;
   public GameObject grey;

   public string spellId;
   
   public bool assignMode = false;
   
   public SpellManager spellManager;

   public ArrowPressed arrowPressed = ArrowPressed.none;
   
   public EventSystem eventSystem;

   private InputActionReference _oldInput;

   public bool _isActive = false;

   private void Awake()
   {
      _inputActions = new InputSystem_Actions();
      _arrowAction = _inputActions.UI.ArrowsSelect;
      _cornerAction = _inputActions.UI.NavigateCorners;
      _focusAction = _inputActions.UI.EquippingFocus;
      
      _oldInput = eventSystem.GetComponent<InputSystemUIInputModule>().move; 
   }

   private void OnEnable()
   {
      _inputActions.Enable();
      _arrowAction.performed += Select;
      _cornerAction.performed += Navigate;
      _focusAction.performed += Focus;

      OnFocusShift += TurnOffAssign;
      
      OnMenuActive += MenuActive;
   }

   private void OnDisable()
   {
      _inputActions.Disable();
      _arrowAction.performed -= Select;
      _cornerAction.performed -= Navigate;
      _focusAction.performed -= Focus;
      
      OnFocusShift -= TurnOffAssign;

      OnMenuActive -= MenuActive;
   }

   private void MenuActive(bool value)
   {
      _isActive = value;
   }

   private void Select(InputAction.CallbackContext ctx)
   {
      if (!assignMode) return;

      Vector2 input = ctx.ReadValue<Vector2>();

      if (input == Vector2.left)
      {
         arrowPressed = ArrowPressed.left;
      }
      else if (input == Vector2.right)
      {
         arrowPressed = ArrowPressed.right;
      }
      else if (input == Vector2.up)
      {
         arrowPressed = ArrowPressed.up;
      }
      else if (input == Vector2.down)
      {
         arrowPressed = ArrowPressed.down;
      }
      
      SetSpell();
      
      TurnOffAssign();

   }

   public void SetSpell()
   {
      spellManager.WhichSpell(arrowPressed, spellId);
   }

   public void EnterAssginMode(string Id)
   {
      if (!_isActive) return;
      
      if (assignMode)
      {
         TurnOffAssign();
         return;
      }
      
      assignMode = true;
      spellId = Id;
      
      assignModeIndicator.SetActive(true);
      grey.SetActive(true);
      eventSystem.GetComponent<InputSystemUIInputModule>().move = null;
   }

   private void Navigate(InputAction.CallbackContext ctx)
   {
      Vector2 input = ctx.ReadValue<Vector2>();
      
      SwitchButtonCorners.OnNavigate?.Invoke(input);
   }

   private void Focus(InputAction.CallbackContext ctx)
   {
      SwitchButtonCorners.OnFocus?.Invoke();
   }

   private void TurnOffAssign()
   {
      assignMode = false;
      grey.SetActive(false);
      assignModeIndicator.SetActive(false);
      eventSystem.GetComponent<InputSystemUIInputModule>().move = _oldInput;
   }


}
