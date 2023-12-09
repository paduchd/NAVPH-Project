using System;
using UnityEngine;

public interface IDamageable
{
    void TakeDamage(int damage)
    {
        throw new NotImplementedException();
    }

    void TakeDamage(int damage, Transform enemy)
    {
        throw new NotImplementedException();
    }
}