using UnityEngine;

public class SetEntryPosition : MonoBehaviour
{
   public void SafeEntryPosition()
   {
      SafeManager.OnAreaChange?.Invoke(gameObject.transform);
   }
}
