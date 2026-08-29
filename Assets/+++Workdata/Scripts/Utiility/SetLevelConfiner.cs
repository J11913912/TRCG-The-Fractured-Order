using Unity.Cinemachine;
using UnityEngine;

public class SetLevelConfiner : MonoBehaviour
{
    public CinemachineCamera playerCamera;

    public void SetNewConfiner(Collider2D confiner)
    {
        playerCamera.GetComponent<CinemachineConfiner2D>().InvalidateCache();
        playerCamera.GetComponent<CinemachineConfiner2D>().BoundingShape2D = confiner;
    }
}