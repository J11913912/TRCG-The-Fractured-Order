using System;
using UnityEngine;

public class CrystalBallBehaviour : MonoBehaviour
{
    private SpriteColorChanger _spriteColorChanger;

    private void Awake()
    {
        _spriteColorChanger = GetComponent<SpriteColorChanger>();
    }

    public void ChangeColor()
    {
        _spriteColorChanger.ColorObject();
    }

    public void DestroyThis()
    {
        Destroy(gameObject);
    }
    
    
}
