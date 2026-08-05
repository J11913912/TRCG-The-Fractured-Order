using System;
using UnityEngine;

public class BasicProjectileSpell : MonoBehaviour
{
    public static Action BaseProjectileSpell;
    public GameObject projectilePrefab;
    
    private PlayerDirection _playerDirection;
    
    private PlayerStates _playerState;
    private PlayerAnimation _playerAnimation;
    private BaseProjectileBehaviour _projectileBehaviour;
    
    private Vector2 _spawnPosition;
    private Vector2 _direction;

    // TODO charging
    
    private void Awake()
    {
        _playerState =  GetComponent<PlayerStates>();
        _playerAnimation = GetComponent<PlayerAnimation>();
    }

    private void OnEnable()
    {
        BaseProjectileSpell += Cast;
    }

    private void OnDisable()
    {
        BaseProjectileSpell -= Cast;
    }

    private void Cast()
    {
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
        
        GameObject projectile = Instantiate(projectilePrefab);
        projectile.transform.position = _spawnPosition;
        
        _projectileBehaviour = projectile.GetComponent<BaseProjectileBehaviour>();
        
        _projectileBehaviour.Shoot(_direction);
        
        _playerAnimation.AnimationSetAction(10);
    }
}
