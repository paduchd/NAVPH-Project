using System;
using UnityEngine;

// Interfaces used by all damageable classes
public interface IDamageable
{
    // TakeDamage method which takes only damage taken as argument
    void TakeDamage(int damage)
    {
        throw new NotImplementedException();
    }

    // Takedamage method which also knockbacks the object in the scene
    void TakeDamage(int damage, Transform enemy)
    {
        throw new NotImplementedException();
    }
}