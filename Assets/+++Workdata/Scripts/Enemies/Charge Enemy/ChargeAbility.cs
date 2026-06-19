using System;
using UnityEngine;

public class ChargeAbility : MonoBehaviour
{
    public Vector2 chargeTarget;

    private Rigidbody2D _rb;

    public float force;
    private void Awake()
    {
        
        _rb = GetComponent<Rigidbody2D>();

        _rb.AddForce(new Vector2(5, 4) * force, ForceMode2D.Impulse);
    }

    public void Charge(Vector2 chargeTarget)
    {
        _rb.AddForce(chargeTarget);
    }
}

