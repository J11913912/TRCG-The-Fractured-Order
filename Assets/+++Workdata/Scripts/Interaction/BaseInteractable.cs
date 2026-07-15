using UnityEngine;
using UnityEngine.Events;

public class BaseInteractable : MonoBehaviour
{
    public UnityEvent OnInteract;
    public UnityEvent OnSelect;
    public UnityEvent OnDeselect;

    public bool canInteract = true;

    protected bool isSelected = false;


    protected void OnDisable()
    {
        Deselected();
    }

    public virtual void Interact()
    {
        OnInteract?.Invoke();
    }

    public virtual void Selected()
    {
        if (isSelected) return;

        isSelected = true;
        OnSelect?.Invoke();
    }

    public virtual void Deselected()
    {
        if (!isSelected) return;
        isSelected = false;
        OnDeselect?.Invoke();
    }
}
