using UnityEngine;

public class FakeBossInteract : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void StartAnimation()
    {
        _animator.SetTrigger("ActionTrigger");
    }
}
