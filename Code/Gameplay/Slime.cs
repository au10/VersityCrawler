using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    private new void Start()
    {        
        EnemyName = "Slime";
        AttackDamage = 3;
        HP = 10;
        base.Start();
    }

    private new void Update()
    {
        if (GetComponent<BoxCollider2D>().IsTouching(PlayerCollider))
        {
            Attack();
        }
        Move(speed);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            NewTarget();
        }
    }
}
