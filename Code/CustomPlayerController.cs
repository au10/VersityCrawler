using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomPlayerController : MonoBehaviour
{
    public static CustomPlayerController instance;
    public Animator animator;
    public float speed;
    public bool canMove;
    public bool drankSpeedPotion;
    public float potionEffectTimer;

    public bool drankInvisPotion;

    private bool isMoving;
    private float xFacePrev;
    private float yFacePrev;
    private float xMoveDir;
    private float yMoveDir;

    private Rigidbody2D rb;
    private Vector2 moveVelocity;

    private SpriteRenderer sprite;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
    }

    private void Start()
    {
        canMove = true;
        rb = GetComponent<Rigidbody2D>();
        animator.SetFloat("xFace", 0);
        animator.SetFloat("yFace", -1);
        sprite = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (drankSpeedPotion)
        {
            if(potionEffectTimer >= 5)
            {
                drankSpeedPotion = false;
                potionEffectTimer = 0;
            }
            else
            {
                moveVelocity = moveInput.normalized * (speed * 1.5f);
                potionEffectTimer += Time.deltaTime;
            } 
        }
        else
        {
            moveVelocity = moveInput.normalized * speed;
        }
        if (drankInvisPotion)
        {
            if (potionEffectTimer >= 5)
            {
                drankInvisPotion = false;
                sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1f);
                potionEffectTimer = 0;
            }
            else
            {
                sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0.5f);
                potionEffectTimer += Time.deltaTime;
            }
        }
        DetermineDirection(moveInput);

        if (canMove)
        {
            rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
            if (moveVelocity.magnitude != 0)
                isMoving = true;
            else
                isMoving = false;
        }
        else
            return;

        if (isMoving)
        {
            animator.SetBool("isMoving", true);
            animator.SetFloat("xMove", xMoveDir);
            animator.SetFloat("yMove", yMoveDir);
        }
        else
        {
            if (xFacePrev == 0 && yFacePrev == 0)
            {
                xFacePrev = 0;
                yFacePrev = -1;
            }
            animator.SetBool("isMoving", false);
            animator.SetFloat("xFace", xFacePrev);
            animator.SetFloat("yFace", yFacePrev);
        }
    }

    private void DetermineDirection(Vector2 worldDirection)
    {
        if (worldDirection.x == 0 && worldDirection.y == 0)
        {
            xMoveDir = 0;
            yMoveDir = 0;
            return;
        }
        else if (worldDirection.x > 0)
        {
            xMoveDir = 1;
            yMoveDir = 0;
            xFacePrev = 1;
            yFacePrev = 0;
            return;
        }
        else if (worldDirection.x < 0)
        {
            xMoveDir = -1;
            yMoveDir = 0;
            xFacePrev = -1;
            yFacePrev = 0;
            return;
        }
        else if (worldDirection.y > 0)
        {
            xMoveDir = 0;
            yMoveDir = 1;
            xFacePrev = 0;
            yFacePrev = 1;
            return;
        }
        else if (worldDirection.y < 0)
        {
            xMoveDir = 0;
            yMoveDir = -1;
            xFacePrev = 0;
            yFacePrev = -1;
            return;
        }
        else
        {
            Debug.LogError("Player direction assigner not working properly");
            return;
        }
    }
}
