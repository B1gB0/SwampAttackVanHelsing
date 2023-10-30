using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyAnimator))]
public class CelebrationState : State
{
    private EnemyAnimator _animator;

    private void Awake()
    {
        _animator = GetComponent<EnemyAnimator>();
    }

    private void OnEnable()
    {
        _animator.SetCelebrate();
    }

    private void OnDisable()
    {
        _animator.StopAnimation();
    }
}