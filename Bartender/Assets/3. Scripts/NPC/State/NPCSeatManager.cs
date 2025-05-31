using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 씬 안에 의자 관리하고 npc한테 비어 있는 의자를 배정하는 매니저

public class NPCSeatManager : MonoBehaviour
{
    public Transform[] seatList;                        // NPC가 사용할 수 있는 의자 Transform 리스트
    private List<Transform> availableSeates = new();    // 사용 가능한 좌석 리스트 


    private void Awake()
    {
        availableSeates.AddRange(seatList);
    }


    //사용 가능한 좌석 중 하나를  NPC한테 할당하고 리스트에서 제거
    public Transform AssignSeat()
    {
        if(availableSeates.Count == 0) return null;

        Transform seat = availableSeates[0];
        availableSeates.RemoveAt(0);
        return seat;
    }

    public void ReleaseSeat(Transform seat)  //좌석을 다시 사용 가능하도록 되돌리는 거
    {
        if (!availableSeates.Contains(seat))
            availableSeates.Add(seat);
    }
}
