using UnityEngine;

public class MoveToSeatState : NpcState
{
    public MoveToSeatState(NPCController npc) : base(npc) { }

    private Collider anchor;   // 도착 전 이동 지점
    private Transform sitPoint; // 도착 후 앉는 위치

    public override void Enter()
    {
        if (npc.npcData.isSeated)
        {
            Debug.LogWarning("이미 착석한 NPC입니다.");
            return;
        }

        // 1. SeatManager에서 좌석 요청
        (anchor, sitPoint) = npc.movementHandler.AssignSeat();

        if (anchor != null && sitPoint != null)
        {
            // 2. Anchor로 이동 시작
            npc.movementHandler.MoveToSeat(anchor, sitPoint);
            npc.animationHandler.SetTrigger("Walk");
            npc.npcData.isMove = true;
        }
        else
        {
            Debug.LogWarning("사용 가능한 좌석이 없습니다.");
        }
    }

    public override void Update()
    {
        if (!npc.npcData.isMove) return;

        // 3. Anchor 도착 확인
        if (npc.movementHandler.HasReachedDestination())
        {
            // 4. 착석 위치로 위치 이동
            npc.movementHandler.UpdateMovementToSitPoint(sitPoint);

            npc.animationHandler.SetAnimation("Walk", false);
            npc.npcData.isArrived = true;
            npc.npcData.isSeated = true;

            // 5. 다음 상태로 전환
            npc.ChangeState(new StandToSit(npc));
        }
    }
}
