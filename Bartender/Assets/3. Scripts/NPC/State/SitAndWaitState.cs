using System.Collections;
using System.Net.NetworkInformation;
using UnityEngine;
using static VFolders.VFoldersCache;

public class SitAndWaitState : NpcState
{
    public SitAndWaitState(NPCController npc) : base(npc) { }

    public override void Enter()
    {
        // 1. ���� ���� ����
        npc.npcData.isSeated = true;

        // 2. �ɱ� �ִϸ��̼� ����
        npc.animationHandler.SetTrigger("StandToSit"); //Trigger �Ķ���� �̸�

        // 2. �ִϸ��̼� ������ ���� Ȯ�� ��
        if (npc.animationHandler.CheckFinishAnimation("isSit"))
        {
            npc.ChangeState(new OrderState(npc));
        }
    }
}
