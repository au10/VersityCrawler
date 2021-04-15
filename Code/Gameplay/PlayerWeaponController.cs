using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{

    public static PlayerWeaponController instance;

    public Weapon equippedWeapon;
    public bool drankStrengthPotion;
    public float potionEffectTimer;
    public Enemy enemyInRange;
    public Animator animator;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        equippedWeapon = null;
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// When player has a weapon equipped and it touching an enemy, then
    /// knockback the enemy and hurt the enemy based on the weapons attack stats
    /// </summary>
    private void Update()
    {
        if (drankStrengthPotion)
        {
            if(potionEffectTimer >= 5f)
            {
                drankStrengthPotion = false;
                potionEffectTimer = 0;
            }
            else
            {
                potionEffectTimer += Time.deltaTime;
            }
        }
        if (Input.GetButtonDown("Attack") && equippedWeapon != null && enemyInRange != null)
        {
            Animator anim = GetComponent<Animator>();
            Vector2 enemyKnockback = new Vector2();
            animator.SetBool("isMoving", false);
            if (!animator.GetBool("isDead"))
            {
                animator.Play("Attack Tree");

                CustomPlayerController.instance.canMove = false;
                switch (anim.GetFloat("xFace"))
                {
                    case 1.0f:
                        enemyKnockback = new Vector2(enemyInRange.gameObject.transform.position.x + 2, enemyInRange.gameObject.transform.position.y);
                        break;
                    case -1.0f:
                        enemyKnockback = new Vector2(enemyInRange.gameObject.transform.position.x - 2, enemyInRange.gameObject.transform.position.y);
                        break;
                    default:
                        switch (anim.GetFloat("yFace"))
                        {
                            case 1.0f:
                                enemyKnockback = new Vector2(enemyInRange.gameObject.transform.position.x, enemyInRange.gameObject.transform.position.y + 2);
                                break;
                            case -1.0f:
                                enemyKnockback = new Vector2(enemyInRange.gameObject.transform.position.x, enemyInRange.gameObject.transform.position.y - 2);
                                break;
                        }
                        break;

                }
                enemyInRange.gameObject.transform.position = enemyKnockback;
                PerformAttack(enemyInRange);
            }
            
            if (animator.GetCurrentAnimatorStateInfo(0).tagHash == 0)
                CustomPlayerController.instance.canMove = true;
        }
    }

    /// <summary>
    /// Checks when enemy is in range
    /// </summary>
    /// <param name="collision">the enemy collider</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<IEnemy>() != null)
        {
            
            enemyInRange = collision.GetComponent<Enemy>();
        }
    }

    /// <summary>
    /// Called when enemy is not touching player
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (enemyInRange != null && enemyInRange == collision.GetComponent<Enemy>())
        {
            enemyInRange = null;
        }
    }

    /// <summary>
    /// Equip weapon to attack enemies
    /// </summary>
    /// <param name="weaponToEquip">the weapon to equip</param>
    public void EquipWeapon(Weapon weaponToEquip)
    {
        if (equippedWeapon != null)
        {
            //Drop equipped weapon
        }

        equippedWeapon = weaponToEquip;
    }

    /// <summary>
    /// Attack the object if it has the enemy interface
    /// </summary>
    /// <param name="enemy">the enemy interface</param>
    public void PerformAttack(IEnemy enemy)
    {
        if (enemy == null)
        {
            Debug.Log("No nearby enemy");
            return;
        }
        equippedWeapon.Attack(enemy);
    }

}
