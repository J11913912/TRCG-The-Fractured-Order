using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class SpellEquipping : MonoBehaviour
{
   private InputSystem_Actions _inputActions;
   private InputAction _arrowAction;
   private InputAction _cornerAction;
   private InputAction _focusAction;

   public string spellId;
   
   public bool assignMode = false;
   
   public SpellManager spellManager;

   public ArrowPressed arrowPressed = ArrowPressed.none;

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
   }

   public void SetSpell()
   {
      spellManager.WhichSpell(arrowPressed, spellId);
   }

   public void EnterAssginMode(string Id)
   {
      assignMode = true;
      spellId = Id;
   }

   private void Navigate(InputAction.CallbackContext ctx)
   {
      Debug.Log("heloo");
      
      Vector2 input = ctx.ReadValue<Vector2>();
      
      SwitchButtonCorners.OnNavigate?.Invoke(input);
   }

   private void Focus(InputAction.CallbackContext ctx)
   {
      Debug.Log("focus");
      
      SwitchButtonCorners.OnFocus?.Invoke();
   }


}
