using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlyerAIState
{
    protected FlyerAI flyerAI;
    protected float timer = 0;
    public FlyerAIState(FlyerAI newAI)
    {
        flyerAI = newAI;
    }

    public void UpdateStateBase()
    {
        timer += Time.fixedDeltaTime;
        UpdateState();
    }

    public void BeginStateBase()
    {
        timer = 0;
        BeginState();
    }

    public abstract void UpdateState();
    public abstract void BeginState();
}
