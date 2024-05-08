using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyerAIIdleState : FlyerAIState
{
    public FlyerAIIdleState(FlyerAI flyerAI) : base(flyerAI) { }

    public override void UpdateState()
    {
        flyerAI.myFlyer.Stop();

        if (flyerAI.GetTarget() != null)
        {
            flyerAI.ChangeState(flyerAI.chaseState);
        }
    }

    public override void BeginState()
    {

    }
}
