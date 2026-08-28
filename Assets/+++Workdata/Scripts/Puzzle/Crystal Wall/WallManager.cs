using System;
using UnityEngine;

public class WallManager : MonoBehaviour
{
    //public static Action SetWallOff;
    public static Action SetWallOn;

    private void OnEnable()
    {
       // SetWallOff += WallOff;
        SetWallOn += WallOn;
    }

    private void OnDisable()
    {
       // SetWallOff -= WallOff;
        SetWallOn -= WallOn;
    }

    private void WallOff()
    {
       ToggleWall.OnWallUsed?.Invoke(false);
    }

    private void WallOn()
    {
        ToggleWall.OnWallUsed?.Invoke(true);
    }
}
