using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;
using Debug = UnityEngine.Debug;

public class BasicAoESpell : MonoBehaviour
{
    public static Action BasicAoE;

    public GameObject targetPrefab;
    
    private GameObject target;

    private bool _castStarted = false;
    private bool _casting = false;
    
    private PlayerStates _playerState;
    private PlayerInput _playerInput;
    
    private PlayerDirection _playerDirection;
    private Vector2 _direction;
    private Vector2 _spawnPosition;

    private float time;
    public float deathTime;

    private void Awake()
    {
        _playerState = GetComponent<PlayerStates>();
        _playerInput = GetComponent<PlayerInput>();
    }
    
    private void OnEnable()
    {
        BasicAoE += Cast;
    }

    private void OnDisable()
    {
        BasicAoE -= Cast;
    }

    private void Update()
    {
        if (_casting)
        {
            time += Time.deltaTime;

            if (time >= deathTime)
            {
                Attack();
                time = 0;
            }
        }
    }

    private void Cast()
    {
        if (!_castStarted && !_casting)
        {
            _casting = true;
            _castStarted = true;
            
            _playerInput.ToggleMovement(false);
        
            _playerDirection = _playerState.GetPlayerDirection();
        
            if (_playerDirection == PlayerDirection.Left)
            {
                _spawnPosition = new Vector2(transform.position.x - 1, transform.position.y);
                _direction = Vector2.left;
            }
            else if (_playerDirection == PlayerDirection.Right)
            {
                _spawnPosition = new Vector2(transform.position.x + 1, transform.position.y);
                _direction = Vector2.right;
            }
            else if (_playerDirection == PlayerDirection.Up)
            {
                _spawnPosition = new Vector2(transform.position.x, transform.position.y + 1);
                _direction = Vector2.up;
            }
            else if (_playerDirection == PlayerDirection.Down)
            {
                _spawnPosition = new Vector2(transform.position.x, transform.position.y - 1);
                _direction = Vector2.down;
            }

            target = Instantiate(targetPrefab);
            target.transform.position = _spawnPosition;
        }
        else
        {
            Attack();
        }
    }

    private void Attack()
    {
        _castStarted = false;
        _casting = false;
            
        Destroy(target);
            
        _playerInput.ToggleMovement(true);
            
        Debug.Log("FIREBALL!!!!!!!!!");
    }
}
