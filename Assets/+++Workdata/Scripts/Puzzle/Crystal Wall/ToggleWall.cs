using UnityEngine;
using System;

public class ToggleWall : MonoBehaviour
{
    public static Action<bool> OnWallUsed;
    
    public bool wallOn;
    public bool wallUsed = true;
    
    public GameObject wall;

    private void OnEnable()
    {
        OnWallUsed += SetWallUsed;
    }

    private void OnDisable()
    {
        OnWallUsed -= SetWallUsed;
    }

    public void SetWallUsed(bool value)
    {
        wallUsed = value;
    }

    public void Toggle()
    {
        if (wallUsed && wallOn)
        {
            wallOn = !wallOn;
            wall.SetActive(wallOn);
            WallManager.SetWallOff?.Invoke();
        }
        else if (!wallUsed && !wallOn)
        {
            wallOn = !wallOn;
            wall.SetActive(wallOn);
            WallManager.SetWallOn?.Invoke();
        }
    }
}
