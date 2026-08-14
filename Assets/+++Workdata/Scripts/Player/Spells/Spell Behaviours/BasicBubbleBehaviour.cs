using UnityEngine;

public class BasicBubbleBehaviour : MonoBehaviour
{
    public void DestroyBubble()                                                                                         // triggered via animation event
    { 
        BasicBubbleSpell.KillBubble?.Invoke();
        Destroy(gameObject);
    }
}
