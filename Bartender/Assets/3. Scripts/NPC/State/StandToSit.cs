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
        yield return null; // ���� ���� ����

        // SitIdle ���·� ���̵� ������ ��ٸ���
        while (!npc.animationHandler.CheckFinishAnimation("SitIdle"))
        {
            yield return null;
        }

        Debug.Log("SitIdle ��!");
        npc.ChangeState(new OrderState(npc));
    }

}
