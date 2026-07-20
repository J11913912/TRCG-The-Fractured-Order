using System;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class ReceiverBehaviour : MonoBehaviour
{
    public bool _receiverOn = false;
    
    public GameObject _light;

    public bool isEmitter;

    public bool isOpposite = false;

    public GameObject beam;
    public GameObject toggledObject;
    
    private void Update()
    {
        _light.SetActive(_receiverOn);

        if (isEmitter)
        {
            beam.SetActive(_receiverOn);
        }
        else
        {
            if (!isOpposite)
            {
                toggledObject.SetActive(_receiverOn);
            }
            else
            {
                toggledObject.SetActive(!_receiverOn);
            }
        }
    }

    public void ToggleReceiver()
    {
        _receiverOn = !_receiverOn;
    }
}
