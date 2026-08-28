using UnityEngine;

public class CrystalWallAnimationEventManager : MonoBehaviour
{
    public ToggleWall toggleWall;

    public void TurnOffWall()
    {
        toggleWall.KillWall();
    }
    
}
