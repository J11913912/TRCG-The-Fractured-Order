using UnityEngine;

public class CrystalGuardBehaviour : MonoBehaviour
{
    public static int Hash_MovementValue = Animator.StringToHash("MovementValue");
    public static int Hash_XDirection = Animator.StringToHash("XDirection");
    public static int Hash_YDirection = Animator.StringToHash("YDirection");
    public static int Hash_ActionID = Animator.StringToHash("ActionID");
    public static int Hash_ActionTrigger = Animator.StringToHash("ActionTrigger");
    
    public float timeToSelfDestruct;
    private float time;
    
    private Animator _animator;
    
    private PlayerStates _playerState;
    private PlayerDirection _direction; 
    
    public bool isAoE = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerState = GameObject.Find("Player").GetComponent<PlayerStates>();
    }
    
    private void Update()
    {
        time += Time.deltaTime;
        if (time >= timeToSelfDestruct)
        {
            _animator.SetTrigger(Hash_ActionTrigger);
            _animator.SetInteger(Hash_ActionID, 100);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _animator.SetTrigger(Hash_ActionTrigger);
        _animator.SetInteger(Hash_ActionID, 100);
    }

    public void DestroyThis()
    {
        Destroy(gameObject);
    }

    public void Shoot(Vector2 direction)
    {
        if (!isAoE)
        {
            UpdateAnimator();
        }
    }

    private void UpdateAnimator()
    {
        _direction = _playerState.GetPlayerDirection();

        switch (_direction)
        {
            case PlayerDirection.Down:
                _animator.SetFloat(Hash_YDirection, -1);
                _animator.SetFloat(Hash_XDirection, 0);
                break;
            
            case PlayerDirection.Left:
                _animator.SetFloat(Hash_YDirection, 0);
                _animator.SetFloat(Hash_XDirection, -1);
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            
            case PlayerDirection.Right:
                _animator.SetFloat(Hash_YDirection, 0);
                _animator.SetFloat(Hash_XDirection, 1);
                gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
                break;
            
            case PlayerDirection.Up:
                _animator.SetFloat(Hash_YDirection, 1);
                _animator.SetFloat(Hash_XDirection, 0);
                break;
        }
    }
}
