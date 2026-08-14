using System;
using UnityEngine;

public class CrystalBallBehaviour : MonoBehaviour
{
    private SpriteColorChanger _spriteColorChanger;

    private void Awake()
    {
        _spriteColorChanger = GetComponent<SpriteColorChanger>();
    }

    public void ChangeColor()                                                                                           // triggered every heal over time
    {
        _spriteColorChanger.ColorObject();
    }

    public void DestroyThis()                                                                                           // triggered via animation event
    {
        Destroy(gameObject);
    }
    
    
}
