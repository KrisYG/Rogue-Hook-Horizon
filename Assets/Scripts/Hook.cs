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

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerObject = GameObject.FindWithTag("Player");
        playerJoint = playerObject.GetComponent<DistanceJoint2D>();
        playerPosition = playerObject.transform.localPosition;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Environment")
        {
            rb.bodyType = RigidbodyType2D.Static;
            playerJoint.distance = Vector2.Distance(playerPosition, transform.localPosition) - 0.25f;

            playerJoint.GetComponent<DistanceJoint2D>().connectedBody = gameObject.GetComponent<Rigidbody2D>();
            playerJoint.enabled = true;
            isConnected = true;
        }

        if (other.gameObject.tag == "Destructable")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
