using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAndMovement : MonoBehaviour
{
    private SpriteRenderer playerSprite;
    private Rigidbody2D rigidBody;
    private Animator animator;

    [Header("Stats")]
    public float speed = 1f;
    public float airSpeed = 0f;
    [SerializeField] float jumpForce = 3;
    [SerializeField] int health = 3;
    [SerializeField] int stamina = 3;

    public enum PlayerMovementType { ground, air };
    [SerializeField] PlayerMovementType movementType = PlayerMovementType.ground;

    [Header("Physics")]
    [SerializeField] LayerMask groundMask;
    [SerializeField] float jumpOffset = -.5f;
    [SerializeField] float jumpRadius = .25f;
    private bool isJumping;
    private float jumpInput;
    private float horizontalInput;

    [Header("Rope Physics")]
    public float swingForce = 4f;
    public Vector2 ropeHook;
    public bool isSwinging;
    public bool groundCheck;


    [Header("Flavor")]
    [SerializeField] string characterName = "Blitz";
    //public GameObject body;

    [Header("Tracked Data")]
    [SerializeField] Vector3 homePosition = Vector3.zero;

    public LineRenderer ropeRenderer;
    public LayerMask ropeLayerMask;
    // [SerializeField] EntitySO entitySO;

    // Rigidbody2D rb;
    SpriteRenderer body;


    void Awake()
    {
        playerSprite = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
        // animator = GetComponent<Animator>();
        body = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        jumpInput = Input.GetAxis("Jump");
        horizontalInput = Input.GetAxis("Horizontal");
        var halfHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y;
        groundCheck = Physics2D.Raycast(new Vector2(transform.localPosition.x, transform.localPosition.y - halfHeight - 0.04f), Vector2.down, 0.025f);
    }

    void FixedUpdate()
    {
        if (horizontalInput < 0f || horizontalInput > 0f)
        {
            // animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
            playerSprite.flipX = horizontalInput > 0f;
            if (isSwinging)
            {
                // animator.SetBool("IsSwinging", true);

                // Get normalized direction vector from player to the hook point
                var playerToHookDirection = (ropeHook - (Vector2)transform.localPosition).normalized;

                // Inverse the direction to get a prependicular direction
                Vector2 perpendicularDirection;
                if (horizontalInput < 0)
                {
                    perpendicularDirection = new Vector2(-playerToHookDirection.y, playerToHookDirection.x);
                    var leftPerpPos = (Vector2)transform.localPosition - perpendicularDirection * -2f;
                    Debug.DrawLine(transform.localPosition, leftPerpPos, Color.green, 0f);
                }
                else
                {
                    perpendicularDirection = new Vector2(playerToHookDirection.y, -playerToHookDirection.x);
                    var rightPerpPos = (Vector2)transform.localPosition + perpendicularDirection * 2f;
                    Debug.DrawLine(transform.localPosition, rightPerpPos, Color.green, 0f);
                }

                var force = perpendicularDirection * swingForce;
                rigidBody.AddForce(force, ForceMode2D.Force);
            }
            else
            {
                // animator.SetBool("IsSwinging", false);
                Vector3 currentVelocity = Vector3.zero;
                Vector3 direction = Vector3.zero;
                currentVelocity = new Vector3(0, rigidBody.velocity.y, 0);
                direction.x = horizontalInput;

                /*
                var groundForce = speed * 2f;
                rigidBody.AddForce(new Vector2((horizontalInput * groundForce - rigidBody.velocity.x) * groundForce, 0));*/
                rigidBody.velocity = (currentVelocity) + (direction * speed); // new Vector2(rigidBody.velocity.x, rigidBody.velocity.y);
                /*if (groundCheck)
                {
                    var groundForce = speed * 2f;
                    rigidBody.AddForce(new Vector2((horizontalInput * groundForce - rigidBody.velocity.x) * groundForce, 0));
                    rigidBody.velocity = new Vector2(rigidBody.velocity.x, rigidBody.velocity.y);
                }*/
            }
        }
        else
        {
            // animator.SetBool("IsSwinging", false);
            // animator.SetFloat("Speed", 0f);
        }

        if (!isSwinging)
        {
            if (!groundCheck) return;

            isJumping = jumpInput > 0f;
            if (isJumping)
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
            }
        }
    }

}
