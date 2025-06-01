using System.Net.NetworkInformation;
using UnityEngine;

public class MoveToSeatState : NpcState
{

    public MoveToSeatState(NPCController npc) : base(npc) { }

    public override void Enter()
    {
        if (npc.npcData.isSeated)
        {
            Debug.LogWarning("�̹� ������ NPC�Դϴ�.");
            return;
        }

        // 1. SeatManager���� �¼� ��û
        Transform assignedSeat = npc.seatManager.AssignSeat();

        if (assignedSeat != null) // 2. �¼� ��ġ�� �̵�
        {
            npc.animationHandler.SetAnimation("Walk", true);
            Debug.Log($"wal");
            npc.movementHandler.MoveTo(assignedSeat.position);
            npc.npcData.isMove = true;
        }
        else
        {
            Debug.LogWarning("��� ������ �¼��� �����ϴ�.");
            // ���� ��õ� �����̳� �ٸ� ��� ���·� ��ȯ ����
        }
    }

    public override void Update()
    {
        // 4. �¼��� �����ߴ��� Ȯ��
        if (npc.npcData.isArrived && npc.movementHandler.HasReachedDestination())
        {
            npc.npcData.isArrived = false;

            // 5. �¼� ���� �� ���� ����
            npc.npcData.isSeated = true;
            npc.animationHandler.SetAnimation("Walk", false);
            npc.ChangeState(new SitAndWaitState(npc));
        }
    }

    public override void Exit()
    {
        // �ʿ� �� Exit ó��
    }
}
