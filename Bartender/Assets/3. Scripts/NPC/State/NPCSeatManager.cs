using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// �� �ȿ� ���� �����ϰ� npc���� ��� �ִ� ���ڸ� �����ϴ� �Ŵ���

public class NPCSeatManager : MonoBehaviour
{
    public Transform[] seatList;                        // NPC�� ����� �� �ִ� ���� Transform ����Ʈ
    private List<Transform> availableSeates = new();    // ��� ������ �¼� ����Ʈ 


    private void Awake()
    {
        availableSeates.AddRange(seatList);
    }


    //��� ������ �¼� �� �ϳ���  NPC���� �Ҵ��ϰ� ����Ʈ���� ����
    public Transform AssignSeat()
    {
        if(availableSeates.Count == 0) return null;

        Transform seat = availableSeates[0];
        availableSeates.RemoveAt(0);
        return seat;
    }

    public void ReleaseSeat(Transform seat)  //�¼��� �ٽ� ��� �����ϵ��� �ǵ����� ��
    {
        if (!availableSeates.Contains(seat))
            availableSeates.Add(seat);
    }
}
