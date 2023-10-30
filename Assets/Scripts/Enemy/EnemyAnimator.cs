using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour
{
    public readonly int Celebrate = Animator.StringToHash(nameof(Celebrate));
    public readonly int Attack = Animator.StringToHash(nameof(Attack));
    public readonly int Idle = Animator.StringToHash(nameof(Idle));
    public readonly int Run = Animator.StringToHash(nameof(Run));

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetIdle()
    {
        _animator.Play(Idle);
    }

    public void SetMove()
    {
        _animator.Play(Run);
    }

    public void SetAttack()
    {
        _animator.Play(Attack);
    }

    public void SetCelebrate()
    {
        _animator.Play(Celebrate);
    }

    public void StopAnimation()
    {
        _animator.StopPlayback();
    }
}
