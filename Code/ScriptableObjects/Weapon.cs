using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon")]
public class Weapon : Item
{
    public WeaponClass weaponClass;
    public WeaponType weaponType;
    
    [SerializeField]
    private short baseDamage;
    public short BaseDamage { get => baseDamage; set => baseDamage = value; }

    public void Attack(IEnemy enemy)
    {
        if (PlayerWeaponController.instance.drankStrengthPotion)
        {
            enemy.HP -= BaseDamage * 2;
        }
        else
        {
            enemy.HP -= BaseDamage;
        }
        if (enemy.HP <= 0)
        {
            enemy.Die();
        }
        else
        {
            Debug.Log("Hit a " + enemy.EnemyName + " for " + BaseDamage + " damage");
            Debug.Log("New HP: " + enemy.HP);
        }
    }
}

public enum WeaponClass
{
    Melee,
    Ranged,
    Magic
}

public enum WeaponType
{
    Sword,
    Bow,
    Axe,
    Staff,
    Fists
}

