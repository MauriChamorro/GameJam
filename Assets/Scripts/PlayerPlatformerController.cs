using UnityEngine;
using Assets.Scripts;

public class PlayerPlatformerController : PhysicsObject
{
    public GameManager _gameManager;

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;

    private SpriteRenderer spriteRenderer;
    private Animator animator;


    // Use this for initialization
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    protected override void ComputeVelocity()
    {
        //vector direction h
        Vector2 movementVector = Vector2.zero;

        movementVector.x = Input.GetAxis("Horizontal");

        if (grounded && (Input.GetAxis("Vertical") > 0 || Input.GetButtonDown("Jump")))
        {
            //add velocity along y axis
            velocity.y = jumpTakeOffSpeed;
        } //cancel jump
        else if (Input.GetButtonUp("Jump"))
        {
            //reduce de velocity when jumpping
            if (velocity.y > 0)
            {
                velocity.y = velocity.y * 0.34f;
            }
        }

        bool flipSprite = (spriteRenderer.flipX ? (movementVector.x > 0.01f) : (movementVector.x < 0.01f));
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        animator.SetBool("Grounded", grounded);
        animator.SetFloat("Movement", Mathf.Abs(velocity.x) / maxSpeed);

        targetVelocity = movementVector * maxSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Trap")
        {
            _gameManager.ReSpawn();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            _gameManager.CheckPointReached(collision.transform);
        }

        if (collision.gameObject.tag == "Home")
        {
            _gameManager.LevelUp();
        }
    }
}