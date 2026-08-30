using System;
using UnityEngine;

public class PutOnHat : MonoBehaviour
{
    public bool _hatOn = false;
    public bool _hatUnlocked = false;

    public void HatUnlock()
    {
        Debug.Log("HatUnlock");
        _hatUnlocked = true;
    }
    
    public void ToggleHat()
    {
        if (!_hatUnlocked) return;
        
        _hatOn = !_hatOn;

        if (_hatOn)
        {
            HatOn();
        }

        if (!_hatOn)
        {
            HatOff();
        }
    }
    
    private void HatOn()
    {
        PlayerAnimation.CustomOn?.Invoke();
    }

    private void HatOff()
    {
        PlayerAnimation.CustomOff?.Invoke();
    }
    
    
}
