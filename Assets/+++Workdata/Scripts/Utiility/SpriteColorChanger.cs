using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteColorChanger : MonoBehaviour
{
    public Color colorSprite;
    public float colorTime;
    
    private SpriteRenderer sprite;
    private Color defaultColors;

    void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        defaultColors = sprite.color;
    }

    public void ColorObject()
    {
        TurnToColor();
        Invoke("TurnNormal", colorTime);
    }
    
    private void TurnToColor()
    {
        sprite.color = colorSprite;
    }

    private void TurnNormal()
    { 
        sprite.color = defaultColors;
    }
}