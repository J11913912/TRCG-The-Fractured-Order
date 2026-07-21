using UnityEngine;
using UnityEngine.Events;

public class SetEndGate : MonoBehaviour
{
    public bool isShielded = false;
    
    public UnityEvent openGate;
    public UnityEvent closeGate;

    public void SetShield(bool value)
    {
        isShielded = value;
    }

    public void ChangeGateState()
    {
        if (isShielded)
        {
            openGate?.Invoke();
        }
        else
        {
            closeGate?.Invoke();
        }
    }
}
