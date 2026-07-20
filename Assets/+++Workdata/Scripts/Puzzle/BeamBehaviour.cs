using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BeamBehaviour : MonoBehaviour
{
    public GameObject beamLength;
    public BoxCollider2D beamCollider;
    
    public LayerMask layerMask;

    public Vector2 direction;
    private Vector2 pos;
    
    public Vector3 raycastPos;
    public Vector3 raycastEnd;
    public Transform target;
    public Transform raycastEndTarget;
    
    public bool foundWall = false;

    public Vector3 hitPoint;

    public Vector3 normalLength;
    public Vector3 normalPos;

    public bool isShort;

    private void Awake()
    {
        normalLength = beamLength.transform.lossyScale;
        normalPos = new Vector3(beamLength.transform.position.x, beamLength.transform.position.y, beamLength.transform.position.z);
    }

    public void SetNewPos(Vector3 pos)
    {
        normalPos = pos;
    }

    private void Update()
    {
        raycastPos = target.position;
        raycastEnd = raycastEndTarget.position;
        
        direction = raycastEnd - raycastPos;
        
        pos = transform.position;
        pos.x = transform.position.x;

        RaycastHit2D hit = Physics2D.Raycast(
            raycastPos,
            direction, 200000000000000000000f,
            layerMask
        );
        
        if (hit.collider)
        {
            foundWall = true;
            hitPoint = hit.point;

            AdjustLength();
        }
        else
        {
            beamLength.transform.localScale = normalLength;

            if (isShort)
            {
                beamLength.transform.position = new Vector3(beamLength.transform.position.x, normalPos.y, beamLength.transform.position.z);
            }
            else
            {
                beamLength.transform.position = new Vector3(normalPos.x, beamLength.transform.position.y, beamLength.transform.position.z);
            }
        }
    }

    private void AdjustLength()
    {
        float distance = Vector2.Distance(hitPoint, gameObject.transform.position);
        
        beamLength.transform.localScale = new Vector3(distance, beamLength.transform.localScale.y, beamLength.transform.localScale.z);
        beamLength.transform.localPosition =  new Vector3(distance / 2, beamLength.transform.localPosition.y, beamLength.transform.localPosition.z);
        
        // beamCollider.size = beamLength.transform.localScale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(raycastPos, raycastEnd * 10000000000f);
    }
    
    
    
    /// FUCKING RAYCAST WONT CHANGE WITH ROTATIONNNNNNNNNNNNNNNNN FIX IT FOR EMITTERS!!!!!!!!!!!!!!!!!!!!!!!!!
    /// AND FIX THAT BEAMS COMES FUCKING ALONNNNNNGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGG!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
}
