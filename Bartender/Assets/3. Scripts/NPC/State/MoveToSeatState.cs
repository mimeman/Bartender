using System.Net.NetworkInformation;
using UnityEngine;

public class MoveToSeatState : NpcState
{
    public MoveToSeatState(NPCController npc) : base(npc) { }

    private Transform moveTarget; // Anchor ��ġ
    private Transform sitPoint;   // ���� �� ���� ��ġ

    public override void Enter()
    {
        if (npc.npcData.isSeated)
        {
            Debug.LogWarning("�̹� ������ NPC�Դϴ�.");
            return;
        }

        // 1. SeatManager���� �¼� ��û
        (Transform anchor, Transform sit) = npc.seatManager.AssignSeatPair();

        if (anchor != null && sit != null)
        {
            moveTarget = anchor;
            sitPoint = sit;

            // 2. �¼����� �̵�
            npc.movementHandler.MoveTo(moveTarget.position);
            npc.animationHandler.SetTrigger("isWalk");
            npc.npcData.isMove = true;
        }
        else
        {
            Debug.LogWarning("��� ������ �¼��� �����ϴ�.");
        }
    }

    public override void Update()
    {
        // 4. �¼��� �����ߴ��� Ȯ��
        if (npc.npcData.isMove && npc.movementHandler.HasReachedDestination())
        {

            // ���� �� �¼� ��ġ�� �ڷ���Ʈ
            npc.transform.position = sitPoint.position;
            npc.transform.rotation = sitPoint.rotation;

            npc.animationHandler.SetAnimation("isWalk", false);
            npc.npcData.isArrived = true;
            npc.npcData.isSeated = true;

            // ���� ���� ��ȯ
            npc.ChangeState(new StandToSit(npc));
        }
    }
}
