using UnityEngine;
using UnityEngine.Events;

public class GeneralAnimationEventManager : MonoBehaviour
{
    public UnityEvent OnAnimationEnd;

    public void AnimationEnd()
    {
        OnAnimationEnd?.Invoke();
    }
}
