using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;

public class SwitchButtonCorners : MonoBehaviour
{
    public static Action<Vector2> OnNavigate;
    public static Action OnFocus;

    public GameObject currentSelectedGameObject;
    public GameObject focusButton;

    public List<GameObject> allButtons;

    private int[,] _corners = new int[2, 2];

    public int currentCorner;

    public Vector2 currentPos;

    public bool justPressed = false;
    public bool focusOn = false;
    public bool justPressedFocus = false;

    private void Awake()
    {
        // currentSelectedGameObject = allButtons[0];
        currentCorner = 0;
        currentPos = Vector2.zero; 
    }

    private void OnEnable()
    {
        OnNavigate += CornerChange;
        OnFocus += EquippingFocusOn;
    }

    private void OnDisable()
    {
        OnNavigate -= CornerChange;
        OnFocus -= EquippingFocusOn;
    }
    

    public void CornerChange(Vector2 input)
    {
        if (justPressed) return;
        if (focusOn) return;
        justPressed = true;

        SpellEquipping.OnFocusShift?.Invoke();
        
        if (input == Vector2.left)
        {
            currentPos.x = currentPos.x ==  0 ? 1 : 0;
            SetNewButtonCorner(currentCorner);
        }
        else if (input == Vector2.right)
        {
            currentPos.x = currentPos.x ==  0 ? 1 : 0;
            SetNewButtonCorner(currentCorner);
        }

        if (input == Vector2.up)
        {
            currentPos.y = currentPos.y ==  0 ? 1 : 0;
            SetNewButtonCorner(currentCorner);
        }
        else if (input == Vector2.down)
        {
            currentPos.y = currentPos.y ==  0 ? 1 : 0;
            SetNewButtonCorner(currentCorner);
        }

     /*   if (input == Vector2.left)
        {
            currentCorner = currentCorner - 1;
            if (currentCorner < 0)
            {
                currentCorner = 3;
            }
            SetNewButton(currentCorner);
        }
        else if (input == Vector2.right)
        {
            currentCorner = currentCorner + 1;
            if (currentCorner > 3)
            {
                currentCorner = 0;
            }
            SetNewButton(currentCorner);
        }

        if (input == Vector2.up)
        {
            if (currentCorner == 2)
            {
                currentCorner = 1;
            }

            if (currentCorner == 3)
            {
                currentCorner = 0;
            }
            SetNewButton(currentCorner);
        }
        else if (input == Vector2.down)
        {
            if (currentCorner == 0)
            {
                currentCorner = 3;
            }

            if (currentCorner == 1)
            {
                currentCorner = 2;
            }
            SetNewButton(currentCorner);
        }
        */
     
        StartCoroutine(PressAgain());
    }

    private IEnumerator PressAgain()
    {
        yield return new WaitForEndOfFrame();
        justPressed = false;
    }

    private void SetNewButtonCorner(int corner)
    {
        Debug.Log("SetNewButton");

        if (currentPos == new Vector2(0, 0))
        {
            currentSelectedGameObject = allButtons[1];
        }
       
        if (currentPos == new Vector2(1, 0))
        {
            currentSelectedGameObject = allButtons[13];
        }
       
        if (currentPos == new Vector2(1, 1))
        {
            currentSelectedGameObject = allButtons[9];
        }
       
        if (currentPos == new Vector2(0, 1))
        {
            currentSelectedGameObject = allButtons[5];
        }
        
        currentSelectedGameObject.GetComponent<Button>().Select();
    }

    private void EquippingFocusOn()
    {
        if (justPressedFocus) return;
        justPressedFocus = true;

        if (!focusOn)
        {
            focusOn = true;

            currentSelectedGameObject = focusButton;
            currentSelectedGameObject.GetComponent<Button>().Select();
            
            SpellEquipping.OnFocusShift?.Invoke();
        }
        else
        {
            EquippingFocusOff();
        }
        
        StartCoroutine(PressAgainFocus());
    }
    
    private void EquippingFocusOff()
    {
        focusOn = false;
        
        currentSelectedGameObject = allButtons[0];
        currentSelectedGameObject.GetComponent<Button>().Select();
    }
    
    private IEnumerator PressAgainFocus()
    {
        yield return new WaitForEndOfFrame();
        justPressedFocus = false;
    }
}
