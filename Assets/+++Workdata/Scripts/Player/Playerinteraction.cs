using System;
using System.Collections.Generic;
using UnityEngine;

public class Playerinteraction : MonoBehaviour
{
    public static Action OnInteract;
    public List<BaseInteractable> _currentInteractables;

    private void OnEnable()
    {
        OnInteract += Interact;
    }

    private void OnDisable()
    {
        OnInteract -= Interact;
    }

    private void Interact()
    {
        if (_currentInteractables.Count < 1) return;
        
        _currentInteractables[0].Interact();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        BaseInteractable _currentBaseInteractable = other.GetComponent<BaseInteractable>();
        if (_currentBaseInteractable)
        {
            _currentInteractables.Add(_currentBaseInteractable);
            _currentInteractables[^1].Selected();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        BaseInteractable _currentBaseInteractable = other.GetComponent<BaseInteractable>();
        if (_currentBaseInteractable)
        {
            _currentBaseInteractable.Deselected();
            _currentInteractables.Remove(_currentBaseInteractable);
        }
    }
}
