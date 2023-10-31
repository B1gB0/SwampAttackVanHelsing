using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerAnimator : PlayerAnimatorData
{
    [SerializeField] private GameObject _hitBoxKnife;

    private PlayerMovement _playerMovement;
    private Player _player;
    private Animator _animator;

    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _player = GetComponent<Player>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        SetAnimation();
    }

    private void SetAnimation()
    {
        if (Input.GetMouseButtonDown(0) && _playerMovement.Velocity.x == 0 && _player.CrossbowIsBuyed)
        {
            _animator.SetTrigger(Shoot);
        }

        if (Input.GetKeyDown(KeyCode.E) && _playerMovement.Velocity.x == 0 && _player.KnifeIsBuyed)
        {
            _hitBoxKnife.SetActive(true);
            _animator.SetTrigger(Attack1);
        }
        else
        {
            _hitBoxKnife.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Q) && _playerMovement.Velocity.x == 0 && _player.KnifeIsBuyed)
        {
            _hitBoxKnife.SetActive(true);
            _animator.SetTrigger(Attack2);
        }
        else
        {
            _hitBoxKnife.SetActive(false);
        }

        if (_playerMovement.Velocity.x == 0)
        {
            _animator.SetBool(isRunning, false);
        }
        else
        {
            _animator.SetBool(isRunning, true);
        }

        if (Input.GetKey(KeyCode.Space) && _playerMovement.IsGrounded)
        {
            _animator.SetTrigger(TakeOff);
        }

        if (_playerMovement.IsGrounded == true)
        {
            _animator.SetBool(isJumping, false);
        }
        else
        {
            _animator.SetBool(isJumping, true);
        }
    }
}
