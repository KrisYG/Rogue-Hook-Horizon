using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Stats")]
    public float speed = 0f;
    [SerializeField] float jumpForce = 10;
    [SerializeField] int health = 3;
    [SerializeField] int stamina = 3;

    public enum EntityMovementType { ground, air, hooked };
    [SerializeField] EntityMovementType movementType = EntityMovementType.ground;

    [Header("Physics")]
    [SerializeField] LayerMask groundMask;
    [SerializeField] float jumpOffset = -.5f;
    [SerializeField] float jumpRadius = .25f;

    [Header("Flavor")]
    [SerializeField] string characterName = "Blitz";
    public GameObject body;

    [Header("Tracked Data")]
    [SerializeField] Vector3 homePosition = Vector3.zero;
    // [SerializeField] EntitySO entitySO;

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {

    }

    void Update()
    {
        /*if(entitySO != null){
            entitySO.health = health;
            entitySO.stamina = stamina;
        }*/
    }

    public void MoveEntity(Vector3 direction)
    {
        if (movementType == EntityMovementType.ground)
        {
            MoveEntityGround(direction);
        }
        /*else if (movementType == EntityMovementType.air)
        {
            MoveEntityAir(direction);
        }
        else if (movementType == EntityMovementType.hooked)
        {
            MovePlayerAir(direction);
        }*/

        //set animation
        /*if (direction != Vector3.zero){
            foreach(AnimationStateChanger asc in animationStateChangers){
                asc.ChangeAnimationState("Walk", speed);
            }
        }else {
            foreach(AnimationStateChanger asc in animationStateChangers){
                asc.ChangeAnimationState("Idle");
            }
        }*/
    }

    public void MoveEntityToward(Vector3 target)
    {
        Vector3 direction = target - transform.position;
        MoveEntity(Vector3.zero);
    }

    public void Stop()
    {
        MoveEntity(Vector3.zero);
    }

    public void MoveEntityGround(Vector3 direction)
    {
        transform.position += direction * Time.deltaTime * speed;
    }

    public void MoveEntityAir()
    {

    }

    public void MovePlayerHooked()
    {

    }
}
