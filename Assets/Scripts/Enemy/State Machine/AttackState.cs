using UnityEngine;

[RequireComponent(typeof(EnemyAnimator))]
public class AttackState : State
{
    [SerializeField] private float _damage;
    [SerializeField] private float _delay;

    private EnemyAnimator _animator;
    private float _lastAttackTime;

    private void Start()
    {
        _animator = GetComponent<EnemyAnimator>();
    }

    private void Update()
    {
        if(_lastAttackTime <= 0)
        {
            Attack(Target);
            _lastAttackTime = _delay;
        }

        _lastAttackTime -= Time.deltaTime;
    }

    private void Attack(Player target)
    {
        _animator.SetAttack();
        target.ApplyDamage(_damage);
    }
}