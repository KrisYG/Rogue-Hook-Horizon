using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GrapplingHook : MonoBehaviour
{
    [SerializeField] GameObject hookPrefab;
    [SerializeField] float hookSpeed = 10;
    [SerializeField] float range = 10;
    public LineRenderer ropeRenderer;
    public LayerMask ropeLayerMasek;
    public float climbSpeed = 3f;
    public DistanceJoint2D ropeJoint;
    public PlayerAndMovement playerMovement;
    private bool ropeAttached;
    private Vector2 playerPosition;
    private Vector3 playerPositionV3;
    private float ropeMaxCastDistance = 20f;
    private bool distanceSet;
    private bool isColliding;
    GameObject hookProjectile;
    PlayerAndMovement playerScript;


    void Awake()
    {
        ropeJoint.enabled = false;
        playerPosition = transform.localPosition;
        playerScript = GetComponent<PlayerAndMovement>();
    }

    public GameObject Launch(Vector3 targetPos)
    {
        GameObject newProjectile = Instantiate(hookPrefab, transform.localPosition, Quaternion.identity);
        newProjectile.transform.rotation = Quaternion.LookRotation(transform.forward, targetPos - transform.localPosition);
        newProjectile.GetComponent<Rigidbody2D>().velocity = newProjectile.transform.up * hookSpeed;
        return newProjectile;
    }

    void Update()
    {
        var worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
        // var facingDirection = worldMousePosition - transform.localPosition;
        // var aimAngle = Mathf.Atan2(facingDirection.y, facingDirection.x);
        /*if (aimAngle < 0f)
        {
            aimAngle = Mathf.PI * 2 + aimAngle;
        }*/

        //var aimDirection = Quaternion.Euler(0, 0, aimAngle * Mathf.Rad2Deg) * Vector2.right;
        playerPosition = transform.localPosition;
        playerPositionV3 = transform.localPosition;

        if (!ropeAttached)
        {
            playerMovement.isSwinging = false;
        }
        else
        {
            playerMovement.isSwinging = true;

        }
        UpdateRopePosition();
        HandleRopeLength();
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("StartMenu");
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            playerScript.SwingAnim();
            if (hookProjectile == null)
            {
                ResetRope();
            }
            if (ropeAttached) return;
            hookProjectile = Launch(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }

        if (hookProjectile != null)
        {
            if (hookProjectile.GetComponent<Hook>().isConnected)
            {
                ropeAttached = true;
                //Debug.Log("Rope Attached here");
            }
            ropeRenderer.SetPosition(1, playerPositionV3);
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            ResetRope();
            playerScript.StopSwingAnim();
        }
    }

    private void ResetRope()
    {
        Destroy(hookProjectile);
        ropeJoint.enabled = false;
        ropeAttached = false;
        playerMovement.isSwinging = false;
        ropeRenderer.positionCount = 2;
        ropeRenderer.SetPosition(0, transform.localPosition);
        ropeRenderer.SetPosition(1, transform.localPosition);
    }

    private void HandleRopeLength()
    {
        if (Input.GetAxis("Vertical") >= 1f && ropeAttached) //&& !isColliding
        {
            // Debug.Log("Moving up");
            ropeJoint.distance -= Time.deltaTime * climbSpeed;
        }
        else if (Input.GetAxis("Vertical") < 0f && ropeAttached && !isColliding)
        {
            // Debug.Log("Moving Down");
            ropeJoint.distance += Time.deltaTime * climbSpeed;
        }
    }

    private void UpdateRopePosition()
    {

    }

    void OnTriggerStay2D(Collider2D colliderStay)
    {
        isColliding = true;
    }

    private void OnTriggerExit2D(Collider2D colliderOnExit)
    {
        isColliding = false;
    }
}
