using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandToSit : NpcState
{
    public StandToSit(NPCController npc) : base(npc) { }
    public override void Enter()
    {
        npc.animationHandler.SetTrigger("StandToSit");

        npc.StartCoroutine(WaitForSitComplete());
    }

    private IEnumerator WaitForSitComplete()
    {
        yield return null; // ���� ���� ����

        // StandToSit ���·� ���̵� ������ ��ٸ���
        while (!npc.animationHandler.CheckFinishAnimation("StandToSit"))
        {
            yield return null;
        }

        Debug.Log("SitIdle ��!");
        npc.ChangeState(new SitIdleState(npc));
    }

}
