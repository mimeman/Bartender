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
            Debug.Log($"�¼����� �̵� ����");

            npc.animationHandler.SetAnimation("Walk", true);
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
        if (npc.movementHandler.HasReachedDestination())
        {
            Debug.Log("���� ����");
            npc.npcData.isArrived = true;

            // 5. �¼� ���� �� ���� ����
            npc.npcData.isSeated = true;
            npc.animationHandler.SetAnimation("Walk", false);
            npc.ChangeState(new StandToSit(npc));
        }
    }

    public override void Exit()
    {
        // �ʿ� �� Exit ó��
    }
}
