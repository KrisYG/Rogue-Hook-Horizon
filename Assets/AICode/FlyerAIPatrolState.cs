using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyerAIPatrolState : FlyerAIState
{
    public FlyerAIPatrolState(FlyerAI flyerAI) : base(flyerAI) { }

    public override void BeginState()
    {
        MoveRandom();
    }
    Vector3 moveVec;
    public override void UpdateState()
    {
        if (timer > 1f)
        {
            timer = 0;

            MoveRandom();
        }

        flyerAI.myFlyer.Move(moveVec);

        if (flyerAI.GetTarget() != null)
        {
            flyerAI.ChangeState(flyerAI.chaseState);
        }
    }

    public void MoveRandom()
    {
        moveVec = (new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0));
    }
}
