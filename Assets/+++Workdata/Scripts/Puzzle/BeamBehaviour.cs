using System;
using UnityEngine;

public class BeamBehaviour : MonoBehaviour
{
    public GameObject beamLength;
    public BoxCollider2D beamCollider;
    
    public LayerMask layerMask;

    private Vector2 direction;
    private Vector2 pos;
    
    public Vector3 raycastPos;
    public Transform target;

    private void Awake()
    {
        raycastPos = target.position;
    }

    private void Update()
    {
        direction = Vector2.right;
        
        pos = transform.position;
        pos.x = transform.position.x;

        RaycastHit2D hit = Physics2D.Raycast(
            raycastPos,
            direction, 200000000000000000000f,
            layerMask
        );
        
        if (hit.collider)
        {
            Debug.Log(hit.collider.gameObject.name);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(raycastPos, raycastPos + Vector3.right * 10000000000f);
    }
}
