using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    float HP { get; set; }
    float AttackDamage { get; set; }
    string EnemyName { get; set; }
    Vector2 TargetPos { get; set; }

    void Attack();
    void Die();
    void Move(float speed);
    
}
