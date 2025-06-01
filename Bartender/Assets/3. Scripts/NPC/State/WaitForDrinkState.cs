using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForDrinkState : NpcState
{
    public WaitForDrinkState(NPCController npc) : base(npc) { }

    public virtual void Enter()
    {
        npc.animationHandler.SetAnimation("SitIdle",true);
    }
    public virtual void Update() { }
    public virtual void Exit() { }

}
