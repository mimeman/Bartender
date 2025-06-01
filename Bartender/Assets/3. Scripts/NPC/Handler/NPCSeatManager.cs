using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// �� �ȿ� ���� �����ϰ� npc���� ��� �ִ� ���ڸ� �����ϴ� �Ŵ���

public class NPCSeatManager : MonoBehaviour
{
    [Header("Seat Management")]
    [Tooltip("NPC�� ����� �� �ִ� ���� Transform ����Ʈ")]
    public Transform[] seatList;      
    
    private List<Transform> availableSeats = new();    // ��� ������ �¼� ����Ʈ
    private Dictionary<Transform, GameObject> occupiedSeats = new(); // ������ �¼��� NPC ����


    private void Awake()
    {
        availableSeats.AddRange(seatList); //��� ���ڸ� ��� ������ ����Ʈ�� �߰�
    }


    //��� ������ �¼� �� �ϳ��� NPC���� �Ҵ��ϰ� ����Ʈ���� ����
    public Transform AssignSeat()
    {
        if (availableSeats.Count == 0)
        {
            return null;  // ��� ������ ���ڰ� ������ return ������
            Debug.LogWarning("��� ������ ���� ����");
        }

        Transform seat = availableSeats[0];
        availableSeats.RemoveAt(0);

        Debug.Log($"���� �Ҵ��: {seat.name}");
        return seat;
    }

    // �¼��� �ٽ� ��� �����ϰ� �ǵ����� �Լ�
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

    // ��� ������ ���� ���� ��ȯ
    public int GetAvailableSeatCount()
    {
        return availableSeats.Count;
    }

    // ��ü ���� ���� ��ȯ
    public int GetTotalSeatCount()
    {
        return seatList.Length;
    }
}
