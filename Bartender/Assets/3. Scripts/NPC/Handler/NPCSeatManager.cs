using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 씬 안에 의자 관리하고 npc한테 비어 있는 의자를 배정하는 매니저

public class NPCSeatManager : MonoBehaviour
{
    [Header("Seat Management")]
    [Tooltip("NPC가 사용할 수 있는 의자 Transform 리스트")]
    public Transform[] seatList;      
    
    private List<Transform> availableSeats = new();    // 사용 가능한 좌석 리스트
    private Dictionary<Transform, GameObject> occupiedSeats = new(); // 점유된 좌석과 NPC 매핑


    private void Awake()
    {
        availableSeats.AddRange(seatList); //모든 의자를 사용 가능한 리스트에 추가
    }


    //사용 가능한 좌석 중 하나를 NPC한테 할당하고 리스트에서 제거
    public Transform AssignSeat()
    {
        if (availableSeats.Count == 0)
        {
            return null;  // 사용 가능한 의자가 없으면 return 시켜줌
            Debug.LogWarning("사용 가능한 의자 없음");
        }

        Transform seat = availableSeats[0];
        availableSeats.RemoveAt(0);

        Debug.Log($"의자 할당됨: {seat.name}");
        return seat;
    }

    // 좌석을 다시 사용 가능하게 되돌리는 함수
    public void ReleaseSeat(Transform seat)
    {
        if (seat == null) return;

        if (!availableSeats.Contains(seat))
        {
            availableSeats.Add(seat);
            occupiedSeats.Remove(seat);
            Debug.Log($"의자 반납: {seat.name}");
        }
    }

    // 사용 가능한 의자 개수 반환
    public int GetAvailableSeatCount()
    {
        return availableSeats.Count;
    }

    // 전체 의자 개수 반환
    public int GetTotalSeatCount()
    {
        return seatList.Length;
    }
}
