using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public static int Hash_MovementValue = Animator.StringToHash("MovementValue");
    public static int Hash_XDirection = Animator.StringToHash("XDirection");
    public static int Hash_YDirection = Animator.StringToHash("YDirection");
    public static int Hash_ActionID = Animator.StringToHash("ActionID");
    public static int Hash_ActionTrigger = Animator.StringToHash("ActionTrigger");


    public static Action<int> OnAnimationAction;

    public List<Animator> _animators = new List<Animator>();

    private PlayerController _playerController;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }

    private void OnEnable()
    {
        OnAnimationAction += AnimationSetAction;
    }

    private void LateUpdate()
    {
        SetMovementAnimationValues();
    }

    private void OnDisable()
    {
        OnAnimationAction -= AnimationSetAction;
    }

    private void SetMovementAnimationValues()
    {
        foreach (var _animator in _animators)
        {
            _animator.SetFloat(Hash_MovementValue, Mathf.Abs(_playerController.Rb.linearVelocity.magnitude));

            if (_playerController.MoveInput.x == 0 && _playerController.MoveInput.y == 0) continue;

            _animator.SetFloat(Hash_XDirection, _playerController.MoveInput.x);
            _animator.SetFloat(Hash_YDirection, _playerController.MoveInput.y);
        }
    }

    private void AnimationSetAction(int ID)
    {
        foreach (var _animator in _animators)
        {
            _animator.SetInteger(Hash_ActionID, ID);
            _animator.SetTrigger(Hash_ActionTrigger);
        }
    }
}
