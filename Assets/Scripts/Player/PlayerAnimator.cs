using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerAnimator : PlayerAnimatorData
{
    [SerializeField] private GameObject _hitBoxKnife;

    private List<int> _attackAnimation = new List<int>();
    private PlayerMovement _playerMovement;
    private Player _player;
    private Animator _animator;

    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _player = GetComponent<Player>();
        _animator = GetComponent<Animator>();
        _attackAnimation.Add(Attack1);
        _attackAnimation.Add(Attack2);
    }

    private void Update()
    {
        SetAnimation();
    }

    private void SetAnimation()
    {
        int numberAttack = Random.Range(0, _attackAnimation.Count);

        if (Input.GetMouseButtonDown(0) && _playerMovement.Velocity.x == 0 &&
           _player.CrossbowIsBuyed && _player.CurrentWeapon.TryGetComponent<Crossbow>(out Crossbow crossbow))
        {
            _animator.SetTrigger(Shoot);
        }

        if (Input.GetKeyDown(KeyCode.Q) && _playerMovement.Velocity.x == 0 &&
           _player.KnifeIsBuyed && _player.CurrentWeapon.TryGetComponent<Knife>(out Knife knife))
        {
            _hitBoxKnife.SetActive(true);
            _animator.SetTrigger(_attackAnimation[numberAttack]);
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
