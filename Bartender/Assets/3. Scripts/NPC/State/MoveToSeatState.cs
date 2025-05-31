using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//npc가 좌석으로 move state
public class MoveToSeatState : NpcState
{
    private bool seatAssigned = false;  //좌석 지정 유무

    public MoveToSeatState(NPCController npc) : base(npc) { }

    public override void Enter()
    {
        // 좌석이 이미 지정되어있는지 확인
        if (npc.npcData.seateTransform)
        {
            NPCSeatManager seatManager = GameObject.FindObjectOfType<NPCSeatManager>();
            if (seatManager != null)
            {
                Transform seat = seatManager.AssignSeat(); //의자 할당
                if (seat != null)
                {
                    npc.npcData.seateTransform = seat;
                    npc.movementHandler.MoveTo(seat.position); //목적지로 이동
                    seatAssigned = true;
                }
                else Debug.LogWarning("의자 없다 더 이상 돌아가");

            }
        }

        //이미 할당된 좌석이 있으면 이동
        else
        {
            npc.movementHandler.MoveTo(npc.npcData.seateTransform.position);
            seatAssigned = true;
        }
    }

    public override void Update() //좌석에 도착했는지 확인 -> 다음 상태로 전환
    {
        if (seatAssigned && npc.movementHandler.HasReachedDestination()) //도착 유무 확인
        {
           // npc.ChangeState(new SitAndWaitState(npc)); // 다음 상태로 전환
        }

    }

    public override void Exit()
    {
        //walk 애니메이션 종료
    }
}
