using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private float ropeMaxCastDistance = 20f;
    private bool distanceSet;
    private bool isColliding;
    GameObject hookProjectile;

    void Awake()
    {
        ropeJoint.enabled = false;
        playerPosition = transform.localPosition;
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
        var facingDirection = worldMousePosition - transform.localPosition;
        var aimAngle = Mathf.Atan2(facingDirection.y, facingDirection.x);
        if (aimAngle < 0f)
        {
            aimAngle = Mathf.PI * 2 + aimAngle;
        }

        var aimDirection = Quaternion.Euler(0, 0, aimAngle * Mathf.Rad2Deg) * Vector2.right;
        playerPosition = transform.localPosition;

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
        HandleInput(aimDirection);
    }

    private void HandleInput(Vector2 aimDirection)
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
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
        }

        /*if (Input.GetKey(KeyCode.LeftShift))
        {
            
        }
        else
        {
            ropeRenderer.enabled = false;
            ropeAttached = false;
            ropeJoint.enabled = false;
        }*/

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            ResetRope();
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
