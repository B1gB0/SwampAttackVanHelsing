using UnityEngine;

[RequireComponent(typeof(EnemyAnimator))]
public class MoveState : State
{
    [SerializeField] private float _speed;

    private EnemyAnimator _animator;

    private void Start()
    {
        _animator = GetComponent<EnemyAnimator>();
    }

    private void Update()
    {
        _animator.SetMove();
        transform.position = Vector2.MoveTowards(transform.position, Target.transform.position, _speed * Time.deltaTime);
    }
}
