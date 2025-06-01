using System.Collections;
using System.Net.NetworkInformation;
using UnityEngine;
using static VFolders.VFoldersCache;

public class SitAndWaitState : NpcState
{
    public SitAndWaitState(NPCController npc) : base(npc) { }

    public override void Enter()
    {
        // 1. 상태 정보 갱신
        npc.npcData.isSeated = true;

        // 2. 앉기 애니메이션 실행
        npc.animationHandler.SetTrigger("StandToSit"); //Trigger 파라미터 이름

        // 2. 애니메이션 끝나는 유무 확인 후
        if (npc.animationHandler.CheckFinishAnimation("isSit"))
        {
            npc.ChangeState(new OrderState(npc));
        }
    }
}
