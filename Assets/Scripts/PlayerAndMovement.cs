using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAndMovement : MonoBehaviour
{
    private SpriteRenderer playerSprite;
    private Rigidbody2D rigidBody;
    private ParticleSystem particleSystem;

    [Header("Stats")]
    public float speed = 1f;
    public float airSpeed = 0f;
    [SerializeField] float jumpForce = 3f;
    [SerializeField] int health = 3;

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
    public bool isSwinging;
    public bool groundCheck;
    private Vector2 ropeHook = Vector2.zero;


    [Header("Flavor")]
    [SerializeField] string characterName = "Blitz";
    [SerializeField] private List<AnimationStateChanger> animationStateChangers;
    public bool hookOut = false;

    [Header("Tracked Data")]
    [SerializeField] Vector3 homePosition = Vector3.zero;

    public LineRenderer ropeRenderer;
    public LayerMask ropeLayerMask;
    private SceneManager sceneManager;

    void Awake()
    {
        playerSprite = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
        particleSystem = GetComponent<ParticleSystem>();
        // animator = GetComponent<Animator>();
        //body = GetComponent<SpriteRenderer>();
        //ropeRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        jumpInput = Input.GetAxis("Jump");
        horizontalInput = Input.GetAxis("Horizontal");
        var halfHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y;
        groundCheck = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - halfHeight - 0.05f - 0.04f), Vector2.down, 0.025f);
        /*if (isSwinging) {
            ropeRenderer.SetPosition(rigidBody.transform.localPosition, )
        }*/
    }

    void FixedUpdate()
    {
        //ropeRenderer.SetPosition(1, rigidBody.transform.localPosition);
        
        if (hookOut)
        {
            foreach (AnimationStateChanger asc in animationStateChangers)
            {
                asc.ChangeAnimationState("Ninja_Swing");
            }
        }
        if (horizontalInput < 0f || horizontalInput > 0f)
        {
            if (!hookOut)
            {
                foreach (AnimationStateChanger asc in animationStateChangers)
                {
                    asc.ChangeAnimationState("Ninja_Walk_Hook");
                }
            }
            playerSprite.flipX = horizontalInput > 0f;
            if (isSwinging)
            {
                // Get normalized direction vector from player to the hook point
                var playerToHookDirection = (ropeHook - (Vector2)transform.localPosition).normalized;

                // Inverse the direction to get a perpendicular direction
                Vector2 perpendicularDirection;
                if (horizontalInput < 0)
                {
                    perpendicularDirection = new Vector2(-playerToHookDirection.y, playerToHookDirection.x);
                    var leftPerpPos = (Vector2)transform.localPosition - perpendicularDirection * -2f;
                    // Debug.DrawLine(transform.localPosition, leftPerpPos, Color.green, 0f);
                }
                else
                {
                    perpendicularDirection = new Vector2(playerToHookDirection.y, -playerToHookDirection.x);
                    var rightPerpPos = (Vector2)transform.localPosition + perpendicularDirection * 2f;
                    // Debug.DrawLine(transform.localPosition, rightPerpPos, Color.green, 0f);
                }

                var force = perpendicularDirection * swingForce;
                rigidBody.AddForce(force, ForceMode2D.Force);
            }
            else
            {
                Vector3 currentVelocity = Vector3.zero;
                Vector3 direction = Vector3.zero;
                currentVelocity = new Vector3(0, rigidBody.velocity.y, 0);
                direction.x = horizontalInput;

                rigidBody.velocity = (currentVelocity) + (direction * speed);
            }
        }
        else
        {
            if (!hookOut)
            {
                foreach (AnimationStateChanger asc in animationStateChangers)
                {
                    asc.ChangeAnimationState("Ninja_Idle_Hook");
                }
            }

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

    public void ReceiveHookPos(Vector2 hookPos)
    {
        ropeHook = hookPos;
    }

    public void SwingAnim()
    {
        hookOut = true;
    }

    public void StopSwingAnim()
    {
        hookOut = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            health -= 1;
            Debug.Log(health);
        }
        if (health <= 0)
        {
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.buildIndex);
        }
    }
}
