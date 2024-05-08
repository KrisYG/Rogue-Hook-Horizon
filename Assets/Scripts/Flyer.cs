using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class Flyer : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    public float speed = 2.5f;
    public bool platformingCreature = false;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector3 offset)
    {
        if (offset != Vector3.zero)
        {
            offset.Normalize();
            Vector3 vel = offset *= speed;
            if (platformingCreature)
            {
                rb.velocity = new Vector2(vel.x, rb.velocity.y);
            }
            else
            {
                rb.velocity = vel;
            }

            if (offset.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
        }
        else
        {
            Stop();
        }
    }

    public void Stop()
    {
        if (platformingCreature)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    public void MoveToward(Vector3 position)
    {
        Move(position - transform.position);
    }

}
