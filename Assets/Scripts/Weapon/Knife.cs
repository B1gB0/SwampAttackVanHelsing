using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : Weapon
{
    private float _damage = 10f;

    public float Damage => _damage;

    public override void Shoot(Transform shootPoint) { }
}
