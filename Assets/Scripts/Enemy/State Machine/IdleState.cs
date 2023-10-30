using UnityEngine;

[RequireComponent(typeof(EnemyAnimator))]
public class IdleState : State
{
    private EnemyAnimator _animator;

    private void Start()
    {
        _animator = GetComponent<EnemyAnimator>();
    }

    private void Update()
    {
        _animator.SetIdle();
    }
}
