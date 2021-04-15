using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy
{
    public PlayerData PlayerToAttack { get; set; }
    public float HP { get; set; }
    public float AttackDamage { get; set; }
    public string EnemyName { get; set; }
    protected BoxCollider2D PlayerCollider { get; set; }

    public bool IsMoving { get; set; }
    public float MoveTimer { get; set; }
    public float StuckTimer { get; set; }
    public float AttackCoolDownTimer { get; set; }
    public bool Attacked { get; set; }
    public Vector2 TargetPos { get; set; }
    public int speed;

    public List<DeathDrop> deathDrops;

    /// <summary>
    /// When enemy is touching player, then lower player's health bar until it reaches zero.
    /// </summary>
    public void Attack()
    {
        if (PlayerData.instance.GetComponent<Animator>().GetBool("isDead"))
            return;

        if (!Attacked)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - 1f);
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            PlayerCollider.GetComponent<PlayerData>().TakeDamage((short)AttackDamage);
            Attacked = true;
            AttackCoolDownTimer = 0;
        }
        
    }

    /// <summary>
    /// When the enemy's health is at zero, then destroy its gameobject
    /// and run item drop algorithm
    /// </summary>
    public void Die()
    {
        Debug.Log("Killed a " + EnemyName + "!");
        Item itemToDrop = CalcItemToDrop();
        if (itemToDrop == null)
            Debug.LogWarning("No item returned");
        else
        {
            float randXPos = transform.position.x + Random.Range(-1,1);
            float randYPos = transform.position.y + Random.Range(-1,1);
            GameObject newObject = Instantiate(itemToDrop.itemObject);
            newObject.transform.position = new Vector2(randXPos, randYPos);
        }
        EnemySpawner.numEnemies--;
        Destroy(gameObject);
    }

    public void Move(float speed)
    {
        if (SeesPlayer(FindObjectOfType<PlayerData>().transform))
        {
            if (!Attacked)
            {
                TargetPos = FindObjectOfType<PlayerData>().transform.position;
            }
            else
            {
                if(AttackCoolDownTimer >= 1)
                {
                    Attacked = false;
                }
                else
                {
                    AttackCoolDownTimer += Time.deltaTime;
                    IsMoving = false;
                }
            }
        }
        MoveTime();
    }

    /// <summary>
    /// Randomly computes new posiiton for enemy to walk to
    /// </summary>
    public void NewTarget()
    {
        float x = Random.Range(transform.position.x - 10, transform.position.x + 10);
        float y = Random.Range(transform.position.y - 10, transform.position.y + 10);
        TargetPos = new Vector2(x, y);
    }

    /// <summary>
    /// If player is too close, then have enemy see player
    /// </summary>
    /// <param name="playerPosition">the position of the player in scene</param>
    /// <returns>true if player is close enough to enemy posiition, false otherwise</returns>
    public bool SeesPlayer(Transform playerPosition)
    {
        if (PlayerData.instance.GetComponent<Animator>().GetBool("isDead"))
            return false;

        if(Vector2.Distance(transform.position, playerPosition.position) <= 10 && !CustomPlayerController.instance.drankInvisPotion)
        {
            IsMoving = true;
            return true;
        }
        return false;
    }
    /// <summary>
    /// Enemy walks in random direction every 3 seconds unless they see the player, then they
    /// go towards player
    /// </summary>
    public void MoveTime()
    {
        if (!IsMoving)
        {
            GetComponent<Animator>().SetBool("isMoving", false);
            if (MoveTimer >= 3)
            {
                IsMoving = true;
                MoveTimer = 0;
                NewTarget();
            }
            else
            {
                MoveTimer += Time.deltaTime;
            }
        }
        else
        {
            if (Vector2.Distance(transform.position, TargetPos) <= 0.1f)
            {
                IsMoving = false;
            }
            transform.position = Vector2.MoveTowards(transform.position, TargetPos, speed * Time.deltaTime);
            GetComponent<Animator>().SetBool("isMoving", true);
        }
    }

    /// <summary>
    /// Determines chance that the enemy drops an item on death based on percent chance
    /// </summary>
    /// <returns>item it will drop, nothing otherwise</returns>
    public Item CalcItemToDrop()
    {
        foreach (DeathDrop deathDrop in deathDrops)
        {
            float rand = Random.Range(1, 101);
            if (rand < deathDrop.chanceToDrop * 100)
            {
                return deathDrop.itemToDrop;
            }
        }
        return null;
    }

    // Start is called before the first frame update
    public void Start()
    {
        BoxCollider2D[] pColliders = GameObject.Find("Player").GetComponents<BoxCollider2D>();
        PlayerCollider = pColliders[1];
    }

    // Update is called once per frame
    public void Update()
    {
        
    }
}
