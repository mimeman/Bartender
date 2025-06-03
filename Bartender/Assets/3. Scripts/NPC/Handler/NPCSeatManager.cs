using System.Collections.Generic;
using UnityEngine;

public class NPCSeatManager : MonoBehaviour
{
    [Header("Seat Management")]
    [Tooltip("NPC�� ����� �� �ִ� ���� Transform ����Ʈ (Chair ��ü)")]
    public Transform[] seatList;

    private List<Transform> availableSeats = new();    // ��� ������ �¼� ����Ʈ
    private Dictionary<Transform, GameObject> occupiedSeats = new(); // ������ �¼��� NPC ����

    private void Awake()
    {
        if (seatList == null || seatList.Length == 0)
        {
            Debug.LogWarning("seatList�� ��� �ֽ��ϴ�. ���� ������ �ν����Ϳ� �����ϼ���.");
            return;
        }

        availableSeats.AddRange(seatList); //�ʱ� ���� ����
    }


    // ��� ������ �¼� �� �ϳ��� NPC���� �Ҵ��ϰ� Anchor + SitPoint ��ȯ

    //(SeatAnchor, SitPoint) Ʃ�� ��ȯ
    public (Transform anchor, Transform sitPoint) AssignSeatPair()
    {
        if (availableSeats.Count == 0)
        {
            Debug.LogWarning("[SeatManager] ��� ������ ���ڰ� �����ϴ�.");
            return (null, null);
        }

        Transform seat = availableSeats[0];
        availableSeats.RemoveAt(0);

        Transform anchor = seat.Find("SeatAnchor");
        Transform sitPoint = seat.Find("SitPoint");

        if (anchor == null || sitPoint == null)
        {
            Debug.LogError($"[SeatManager] '{seat.name}'�� SeatAnchor �Ǵ� SitPoint�� �����ϴ�. �ڽ� ��ü Ȯ�� �ʿ�.");
            return (null, null);
        }

        Debug.Log($"[SeatManager] ���� �Ҵ�: {seat.name} / Anchor: {anchor.name} / SitPoint: {sitPoint.name}");
        return (anchor, sitPoint);
    }


    // �¼��� �ٽ� ��� �����ϵ��� �ǵ���
    public void ReleaseSeat(Transform seat)
    {
        if (seat == null) return;

        if (!availableSeats.Contains(seat))
        {
            availableSeats.Add(seat);
            occupiedSeats.Remove(seat);
            Debug.Log($"���� �ݳ�: {seat.name}");
        }
    }

    public int GetAvailableSeatCount() => availableSeats.Count;
    public int GetTotalSeatCount() => seatList.Length;
}
