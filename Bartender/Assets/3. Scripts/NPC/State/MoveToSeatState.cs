using System.Net.NetworkInformation;
using UnityEngine;

public class MoveToSeatState : NpcState
{
    public MoveToSeatState(NPCController npc) : base(npc) { }

    private Transform moveTarget; // Anchor 위치
    private Transform sitPoint;   // 도착 후 앉을 위치

    public override void Enter()
    {
        if (npc.npcData.isSeated)
        {
            Debug.LogWarning("이미 착석한 NPC입니다.");
            return;
        }

        // 1. SeatManager에서 좌석 요청
        (Transform anchor, Transform sit) = npc.seatManager.AssignSeatPair();

        if (anchor != null && sit != null)
        {
            moveTarget = anchor;
            sitPoint = sit;

            // 2. 좌석으로 이동
            npc.movementHandler.MoveTo(moveTarget.position);
            npc.animationHandler.SetTrigger("isWalk");
            npc.npcData.isMove = true;
        }
        else
        {
            Debug.LogWarning("사용 가능한 좌석이 없습니다.");
        }
    }

    public override void Update()
    {
        // 4. 좌석에 도착했는지 확인
        if (npc.npcData.isMove && npc.movementHandler.HasReachedDestination())
        {

            // 도착 → 좌석 위치로 텔레포트
            npc.transform.position = sitPoint.position;
            npc.transform.rotation = sitPoint.rotation;

            npc.animationHandler.SetAnimation("isWalk", false);
            npc.npcData.isArrived = true;
            npc.npcData.isSeated = true;

            // 다음 상태 전환
            npc.ChangeState(new StandToSit(npc));
        }
    }
}
