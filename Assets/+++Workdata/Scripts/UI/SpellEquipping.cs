using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;


public class SpellEquipping : MonoBehaviour
{
   private InputSystem_Actions _inputActions;
   private InputAction _arrowAction;
   private InputAction _cornerAction;
   private InputAction _focusAction;
   
   public GameObject assignModeIndicator;

   public string spellId;
   
   public bool assignMode = false;
   
   public SpellManager spellManager;

   public ArrowPressed arrowPressed = ArrowPressed.none;
   
   public EventSystem eventSystem;

   private InputActionReference _oldInput;

   private void Awake()
   {
      _inputActions = new InputSystem_Actions();
      _arrowAction = _inputActions.UI.ArrowsSelect;
      _cornerAction = _inputActions.UI.NavigateCorners;
      _focusAction = _inputActions.UI.EquippingFocus;
   }

   private void OnEnable()
   {
      _inputActions.Enable();
      _arrowAction.performed += Select;
      _cornerAction.performed += Navigate;
      _focusAction.performed += Focus;
   }

   private void OnDisable()
   {
      _inputActions.Disable();
      _arrowAction.performed -= Select;
      _cornerAction.performed -= Navigate;
      _focusAction.performed -= Focus;
   }

   // TODO Back button assignmode
   // TODO ausgrauen
   // TODO assignmode indicator
   

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
      
      assignMode = false;
      assignModeIndicator.SetActive(false);
      eventSystem.GetComponent<InputSystemUIInputModule>().move = _oldInput ;

   }

   public void SetSpell()
   {
      spellManager.WhichSpell(arrowPressed, spellId);
   }

   public void EnterAssginMode(string Id)
   {
      assignMode = true;
      spellId = Id;
      
      assignModeIndicator.SetActive(true);

      _oldInput = eventSystem.GetComponent<InputSystemUIInputModule>().move; 
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


}
