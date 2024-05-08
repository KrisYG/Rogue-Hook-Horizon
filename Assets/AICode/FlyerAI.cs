using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyerAI : MonoBehaviour
{
    public Flyer myFlyer;
    public PlayerAndMovement targetPlayer;
    [SerializeField] private List<AnimationStateChanger> animationStateChangers;

    //[Header("Config")]
    //public LayerMask obstacles;
    public float sightDistance = 5;


    //State Machine =======================
    //States go here
    FlyerAIState currentState;
    public FlyerAIIdleState idleState { get; private set; }
    public FlyerAIChaseState chaseState { get; private set; }
    public FlyerAIPatrolState patrolState { get; private set; }


    public void ChangeState(FlyerAIState newState)
    {
        currentState = newState;
        currentState.BeginStateBase();
    }

    void Start()
    {
        idleState = new FlyerAIIdleState(this);
        chaseState = new FlyerAIChaseState(this);
        patrolState = new FlyerAIPatrolState(this);
        currentState = idleState;
    }

    void FixedUpdate()
    {
        currentState.UpdateStateBase(); //work the current state
        if (currentState == idleState || currentState == patrolState)
        {
            foreach (AnimationStateChanger asc in animationStateChangers)
            {
                asc.ChangeAnimationState("Flyer_Idle");
            }
        }
        else if (currentState == chaseState)
        {
            foreach (AnimationStateChanger asc in animationStateChangers)
            {
                asc.ChangeAnimationState("Flyer_Chase");
            }
        }
    }

    public PlayerAndMovement GetTarget()
    {
        if (Vector3.Distance(transform.position, targetPlayer.transform.position) < sightDistance)
        {
            return targetPlayer;
        }
        else
        {
            return null;
        }
    }
}
