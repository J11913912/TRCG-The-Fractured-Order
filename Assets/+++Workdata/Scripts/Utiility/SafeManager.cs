using System;
using UnityEngine;

public class SafeManager : MonoBehaviour
{
    public static Action<Transform> OnAreaChange;
    public static Action OnReset;
    
    public Transform currentReset;
    public GameObject player;

    private void OnEnable()
    {
        OnAreaChange += ChangeReset;
        OnReset += ResetPlayer;
    }

    private void OnDisable()
    {
        OnAreaChange -= ChangeReset;
        OnReset -= ResetPlayer;
    }

    private void ChangeReset(Transform newReset)
    {
        currentReset = newReset;
    }

    public void ResetPlayer()
    {
        player.transform.position = currentReset.position;
    }
    
}
