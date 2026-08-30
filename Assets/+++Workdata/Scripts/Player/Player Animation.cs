using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public static Action CustomOn;
    public static Action CustomOff;
    
    public static int Hash_MovementValue = Animator.StringToHash("MovementValue");
    public static int Hash_XDirection = Animator.StringToHash("XDirection");
    public static int Hash_YDirection = Animator.StringToHash("YDirection");
    public static int Hash_ActionID = Animator.StringToHash("ActionID");
    public static int Hash_ActionTrigger = Animator.StringToHash("ActionTrigger");


    public static Action<int> OnAnimationAction;

    public List<Animator> _animators = new List<Animator>();
    
    public Animator _hatCustom;
    public Animator _hat;
    public Animator _bodyCustom;
    public Animator _body;

    private PlayerController _playerController;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>(); 
    }

    private void OnEnable()
    {
        OnAnimationAction += AnimationSetAction;
        CustomOn += PutOnCustom;
        CustomOff += PutOnNormal;
    }

    private void LateUpdate()
    {
        SetMovementAnimationValues();
    }

    private void OnDisable()
    {
        OnAnimationAction -= AnimationSetAction;
        CustomOn -= PutOnCustom;
        CustomOff -= PutOnNormal;
    }

    private void PutOnCustom()
    {
        _animators.RemoveAt(0);
        _animators.RemoveAt(1);
        
        _animators.Add(_bodyCustom);
        _animators.Add(_hatCustom);
        
        _bodyCustom.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        _hatCustom.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        
        _body.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        _hat.gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    private void PutOnNormal()
    {
        _animators.RemoveAt(0);
        _animators.RemoveAt(1);
        
        _animators.Add(_body);
        _animators.Add(_hat);
        
        _bodyCustom.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        _hatCustom.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        
        _body.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        _hat.gameObject.GetComponent<SpriteRenderer>().enabled = true;
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

    public void AnimationSetAction(int ID)
    {
        foreach (var _animator in _animators)
        {
            _animator.SetInteger(Hash_ActionID, ID);
            _animator.SetTrigger(Hash_ActionTrigger);
        }
    }
    
    public void AnimationSetBool(string boolInQuestion, bool value)
    {
        foreach (var _animator in _animators)
        {
            _animator.SetBool(boolInQuestion, value);
        }
    }
}
