using System;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class ReceiverBehaviour : MonoBehaviour
{
    private bool _receiverOn = false;
    
    public GameObject _light;

    public bool isEmitter;

    public GameObject beam;
    
    private void Update()
    {
        _light.SetActive(_receiverOn);

        if (isEmitter)
        {
            beam.SetActive(_receiverOn);
        }
            
    }

    public void ToggleReceiver()
    {
        _receiverOn = !_receiverOn;
    }
}
