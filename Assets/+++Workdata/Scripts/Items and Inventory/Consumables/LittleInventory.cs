using System;
using TMPro;
using UnityEngine;

public class LittleInventory : MonoBehaviour
{
    public static Action<string> SetDescription;
    public static Action<string> SetHeader;
    public TextMeshProUGUI description;
    public TextMeshProUGUI header;

    private void OnEnable()
    {
        SetDescription += DisplayDesc;
        SetHeader += DisplayHead;
    }

    private void OnDisable()
    {
        SetDescription -= DisplayDesc;
        SetHeader -= DisplayHead;
    }

    private void DisplayDesc(string desc)
    {
        description.SetText(desc);
    }
    
    private void DisplayHead(string heading)
    {
        header.SetText(heading);
    }
}
