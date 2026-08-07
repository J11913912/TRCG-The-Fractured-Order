using UnityEngine;

public class BasicBubbleBehaviour : MonoBehaviour
{
    public void DestroyBubble()
    { 
        BasicBubbleSpell.KillBubble?.Invoke();
        Destroy(gameObject);
    }
}
