using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//npc�� �¼����� move state
public class MoveToSeatState : NpcState
{
    private bool seatAssigned = false;  //�¼� ���� ����

    public MoveToSeatState(NPCController npc) : base(npc) { }

    public override void Enter()
    {
        // �¼��� �̹� �����Ǿ��ִ��� Ȯ��
        if (npc.npcData.seateTransform)
        {
            NPCSeatManager seatManager = GameObject.FindObjectOfType<NPCSeatManager>();
            if (seatManager != null)
            {
                Transform seat = seatManager.AssignSeat(); //���� �Ҵ�
                if (seat != null)
                {
                    npc.npcData.seateTransform = seat;
                    npc.movementHandler.MoveTo(seat.position); //�������� �̵�
                    seatAssigned = true;
                }
                else Debug.LogWarning("���� ���� �� �̻� ���ư�");

            }
        }

        //�̹� �Ҵ�� �¼��� ������ �̵�
        else
        {
            npc.movementHandler.MoveTo(npc.npcData.seateTransform.position);
            seatAssigned = true;
        }
    }

    public override void Update() //�¼��� �����ߴ��� Ȯ�� -> ���� ���·� ��ȯ
    {
        if (seatAssigned && npc.movementHandler.HasReachedDestination()) //���� ���� Ȯ��
        {
           // npc.ChangeState(new SitAndWaitState(npc)); // ���� ���·� ��ȯ
        }

    }

    public override void Exit()
    {
        //walk �ִϸ��̼� ����
    }
}
