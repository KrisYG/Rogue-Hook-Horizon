using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyerAIChaseState : FlyerAIState
{
    public FlyerAIChaseState(FlyerAI flyerAI) : base(flyerAI) { }

    public override void BeginState()
    {

    }

    public override void UpdateState()
    {
        if (flyerAI.GetTarget() != null)
        {
            flyerAI.myFlyer.MoveToward(flyerAI.GetTarget().transform.position);
        }
        else
        {
            flyerAI.ChangeState(flyerAI.patrolState);
        }
    }
}
