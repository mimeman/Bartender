using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandToSit : NpcState
{
    public StandToSit(NPCController npc) : base(npc) { }
    public override void Enter()
    {
        npc.npcData.isSeated = true;
        npc.animationHandler.SetTrigger("StandToSit");

        npc.StartCoroutine(WaitForSitComplete());
    }

    private IEnumerator WaitForSitComplete()
    {
        yield return null; // 상태 전이 보장

        // SitIdle 상태로 전이될 때까지 기다리기
        while (!npc.animationHandler.CheckFinishAnimation("SitIdle"))
        {
            yield return null;
        }

        Debug.Log("SitIdle 끝!");
        npc.ChangeState(new OrderState(npc));
    }

}
