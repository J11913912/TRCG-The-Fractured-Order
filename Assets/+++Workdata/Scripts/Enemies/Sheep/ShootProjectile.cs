using System;
using System.Collections;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    public float speed;

    public Rigidbody2D _rb;

    public void Shoot(Vector2 direction)
    {
        _rb.linearVelocity = direction * speed;
    }

    public void StartDeathCountdown()
    {
        StartCoroutine(Death());
    }

    private IEnumerator Death()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    public void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
