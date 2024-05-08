using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hook : MonoBehaviour
{
    Rigidbody2D rb;
    GameObject playerObject;
    DistanceJoint2D playerJoint;
    public bool isConnected;
    Vector2 playerPosition;
    LineRenderer ropeRenderer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerObject = GameObject.FindWithTag("Player");
        playerJoint = playerObject.GetComponent<DistanceJoint2D>();
        playerPosition = playerObject.transform.localPosition;
        ropeRenderer = playerObject.GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (ropeRenderer.enabled)
        {
            ropeRenderer.SetPosition(1, playerPosition);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Environment")
        {
            rb.bodyType = RigidbodyType2D.Static;

            PlayerAndMovement playerScript = playerObject.GetComponent<PlayerAndMovement>();
            if (playerScript != null)
            {
                playerScript.ReceiveHookPos(transform.localPosition);
            }

            playerJoint.distance = Vector2.Distance(playerPosition, transform.localPosition) - 0.25f;
            playerJoint.connectedBody = gameObject.GetComponent<Rigidbody2D>();
            playerJoint.enabled = true;
            isConnected = true;

            ropeRenderer.enabled = true;
            ropeRenderer.SetPosition(0, rb.transform.localPosition);
        }

        if (other.gameObject.tag == "Destructable")
        {
            other.GetComponent<AudioSource>().Play();
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }

        if (other.gameObject.tag == "Terrain")
        {
            Destroy(this.gameObject);
        }
    }
}
