using System;
using TMPro;
using UnityEngine;

public class LittleInventory : MonoBehaviour
{
    public static Action<string> SetDescription;
    public TextMeshProUGUI description;

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        SetDescription += DisplayDesc;
    }

    private void OnDisable()
    {
        SetDescription -= DisplayDesc;
    }

    private void DisplayDesc(string desc)
    {
        description.SetText(desc);
    }
}
