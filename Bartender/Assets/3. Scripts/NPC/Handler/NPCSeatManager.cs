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


    // ��� ������ �¼� �� �ϳ��� NPC���� �Ҵ��ϰ� AnchorCollider + SitPoint ��ȯ
    public (Collider anchorCollider, Transform sitPoint) AssignSeatPair()
    {
        if (availableSeats.Count == 0)
        {
            Debug.LogWarning("[SeatManager] ��� ������ ���ڰ� �����ϴ�.");
            return (null, null);
        }

        Transform seat = availableSeats[0];
        availableSeats.RemoveAt(0);

        Transform anchorTransform = seat.Find("SeatAnchor");
        Collider anchorCollider = anchorTransform?.GetComponent<Collider>();
        Transform sitPoint = seat.Find("SitPoint");

        if (anchorCollider == null || sitPoint == null)
        {
            Debug.LogError($"[SeatManager] '{seat.name}'�� SeatAnchor Collider �Ǵ� SitPoint�� �����ϴ�.");
            return (null, null);
        }

        Debug.Log($"[SeatManager] ���� �Ҵ� �Ϸ�: {seat.name} / Anchor: {anchorCollider.name} / SitPoint: {sitPoint.name}");
        return (anchorCollider, sitPoint);
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
