using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxKnife : MonoBehaviour
{
    [SerializeField] private Knife _knife;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(_knife.Damage);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(_knife.Damage);
        }
    }
}
