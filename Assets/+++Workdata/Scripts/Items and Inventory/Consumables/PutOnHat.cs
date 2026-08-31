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
        PlayerPrefs.SetInt("HatUnlocked", 1);
        PlayerPrefs.Save();
    }

    public void SetHat()
    {
        if (PlayerPrefs.GetInt("HatUnlocked") == 1)
        {
            _hatUnlocked = true;
        }
        else
        {
            _hatUnlocked = false;
        }

        if (PlayerPrefs.GetInt("HatOn") == 1)
        {
            _hatOn = true; 
            HatOn();
        }
        else
        {
            _hatOn = false;
            HatOff();
        }
           
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
        PlayerPrefs.SetInt("HatOn", 1);
        PlayerPrefs.Save();
    }

    private void HatOff()
    {
        PlayerAnimation.CustomOff?.Invoke();
        PlayerPrefs.SetInt("HatOn", 0);
        PlayerPrefs.Save();
    }
    
    
}
