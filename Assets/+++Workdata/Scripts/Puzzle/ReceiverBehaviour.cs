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
    public GameObject newBeam;
    
    private GameObject _currentBeam;
    public GameObject lightOn;
    public GameObject lightOff;
    
    public GameObject toggledObject;

    private void Awake()
    {
        _currentBeam = beam;
    }

    private void Update()
    {
        _light.SetActive(_receiverOn);
        lightOn.SetActive(_receiverOn);
        lightOff.SetActive(!_receiverOn);

        if (isEmitter)
        {
            _currentBeam.SetActive(_receiverOn);
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

    public void SetNewBeam()
    {
        _currentBeam = newBeam;
        beam.SetActive(false);
        newBeam.SetActive(true);
    }
    
    public void SetOldBeam()
    {
        _currentBeam = beam;
    }

    public void SetReceiverState(bool value)
    {
        _receiverOn = value;
    }
}
