using System.Net.NetworkInformation;
using UnityEngine;

public class MoveToSeatState : NpcState
{

    public MoveToSeatState(NPCController npc) : base(npc) { }

    public override void Enter()
    {
        if (npc.npcData.isSeated)
        {
            Debug.LogWarning("이미 착석한 NPC입니다.");
            return;
        }

        // 1. SeatManager에서 좌석 요청
        Transform assignedSeat = npc.seatManager.AssignSeat();

        if (assignedSeat != null) // 2. 좌석 위치로 이동
        {
            npc.animationHandler.SetAnimation("Walk", true);
            Debug.Log($"wal");
            npc.movementHandler.MoveTo(assignedSeat.position);
            npc.npcData.isMove = true;
        }
        else
        {
            Debug.LogWarning("사용 가능한 좌석이 없습니다.");
            // 이후 재시도 로직이나 다른 대기 상태로 전환 가능
        }
    }

    public override void Update()
    {
        // 4. 좌석에 도착했는지 확인
        if (npc.npcData.isArrived && npc.movementHandler.HasReachedDestination())
        {
            npc.npcData.isArrived = false;

            // 5. 좌석 도착 → 상태 갱신
            npc.npcData.isSeated = true;
            npc.animationHandler.SetAnimation("Walk", false);
            npc.ChangeState(new SitAndWaitState(npc));
        }
    }

    public override void Exit()
    {
        // 필요 시 Exit 처리
    }
}
