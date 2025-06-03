using System.Collections.Generic;
using UnityEngine;

public class NPCSeatManager : MonoBehaviour
{
    [Header("Seat Management")]
    [Tooltip("NPC가 사용할 수 있는 의자 Transform 리스트 (Chair 본체)")]
    public Transform[] seatList;

    private List<Transform> availableSeats = new();    // 사용 가능한 좌석 리스트
    private Dictionary<Transform, GameObject> occupiedSeats = new(); // 점유된 좌석과 NPC 매핑

    private void Awake()
    {
        if (seatList == null || seatList.Length == 0)
        {
            Debug.LogWarning("seatList가 비어 있습니다. 의자 정보를 인스펙터에 설정하세요.");
            return;
        }

        availableSeats.AddRange(seatList); //초기 의자 설정
    }


    // 사용 가능한 좌석 중 하나를 NPC에게 할당하고 Anchor + SitPoint 반환

    //(SeatAnchor, SitPoint) 튜플 반환
    public (Transform anchor, Transform sitPoint) AssignSeatPair()
    {
        if (availableSeats.Count == 0)
        {
            Debug.LogWarning("[SeatManager] 사용 가능한 의자가 없습니다.");
            return (null, null);
        }

        Transform seat = availableSeats[0];
        availableSeats.RemoveAt(0);

        Transform anchor = seat.Find("SeatAnchor");
        Transform sitPoint = seat.Find("SitPoint");

        if (anchor == null || sitPoint == null)
        {
            Debug.LogError($"[SeatManager] '{seat.name}'에 SeatAnchor 또는 SitPoint가 없습니다. 자식 객체 확인 필요.");
            return (null, null);
        }

        Debug.Log($"[SeatManager] 의자 할당: {seat.name} / Anchor: {anchor.name} / SitPoint: {sitPoint.name}");
        return (anchor, sitPoint);
    }


    // 좌석을 다시 사용 가능하도록 되돌림
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

    public int GetAvailableSeatCount() => availableSeats.Count;
    public int GetTotalSeatCount() => seatList.Length;
}
