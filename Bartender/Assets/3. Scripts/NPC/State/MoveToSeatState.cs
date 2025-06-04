using UnityEngine;

public class MoveToSeatState : NpcState
{
    public MoveToSeatState(NPCController npc) : base(npc) { }

    private Collider anchor;   // ���� �� �̵� ����
    private Transform sitPoint; // ���� �� �ɴ� ��ġ

    public override void Enter()
    {
        if (npc.npcData.isSeated)
        {
            Debug.LogWarning("�̹� ������ NPC�Դϴ�.");
            return;
        }

        // 1. SeatManager���� �¼� ��û
        (anchor, sitPoint) = npc.movementHandler.AssignSeat();

        if (anchor != null && sitPoint != null)
        {
            // 2. Anchor�� �̵� ����
            npc.movementHandler.MoveToSeat(anchor, sitPoint);
            npc.animationHandler.SetTrigger("Walk");
            npc.npcData.isMove = true;
        }
        else
        {
            Debug.LogWarning("��� ������ �¼��� �����ϴ�.");
        }
    }

    public override void Update()
    {
        if (!npc.npcData.isMove) return;

        if (npc.movementHandler.HasReachedDestination()) //�¼� ���� Ȯ��
        {
            npc.movementHandler.TeleportToSitPoint(); // ���� ��ġ�� ����

            npc.animationHandler.SetAnimation("Walk", false);
            npc.npcData.isArrived = true;
            npc.npcData.isSeated = true;

            npc.ChangeState(new StandToSit(npc));
        }
    }
}
