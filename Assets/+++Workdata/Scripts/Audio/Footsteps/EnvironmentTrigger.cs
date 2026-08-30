using UnityEngine;

public class EnvironmentTrigger : MonoBehaviour
{
    public FootstepSoundArea footstepSoundArea;
    
    [Min(0)]
    public int priority = 0;
}
